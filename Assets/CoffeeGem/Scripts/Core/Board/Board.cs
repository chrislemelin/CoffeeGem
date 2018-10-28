using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Board : MonoBehaviour {

    [SerializeField]
    private int height = 5;
    [SerializeField]
    private int width = 6;
    [SerializeField]
    private List<IBoardEntity> availibleGems = new List<IBoardEntity>();
    [SerializeField]
    private GameObject tile;
    [SerializeField]
    const float fallingSpeed = 5;
    [SerializeField]
    private float displaceTime = .25f;

    [SerializeField]
    private GameObject bEHolder;
    [SerializeField]
    private GameObject tileHolder;
    [SerializeField]
    private Score score;

    [SerializeField]
    private GameObject scoreFadePF;

    [SerializeField]
    private AudioClip scoreSound;

    private float gemHeight;
    private float gemWidth;
    private int checkingMatchCoroutines = 0;

    private List<IBoardEntity> previewBoardEntities = new List<IBoardEntity>();
    private List<TileOnHover> tiles = new List<TileOnHover>();
    private Ingredient selectedIngredient;

    private Dictionary<Position, IBoardEntity> board = new Dictionary<Position, IBoardEntity>();
    private Boolean sendToCustomer;
    private List<Match> currentMatches = new List<Match>();

    // Use this for initialization
    void Start () {
        RectTransform rt = (RectTransform)availibleGems[0].gameObject.transform;
        gemWidth = rt.rect.width;
        gemHeight = rt.rect.height;

        selectedIngredient = new BasicRedIngredient();
        populateBoard();
        StartCoroutine(checkMatchesAfter(1));
    }

    public void setIngredient(Ingredient ingredient) {
        selectedIngredient = ingredient;
    }

    private List<Position> getEmptyPositions () {
        List<Position> emptyPositions = new List<Position>();

        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                Position checkPosition = new Position(x, y);
                if (!board.ContainsKey(checkPosition)) {
                    emptyPositions.Add(checkPosition);
                }
            }
        }
        return emptyPositions;
    }

    private void populateBoard() {
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                addBoardEntity(availibleGems[UnityEngine.Random.Range(0, availibleGems.Count)], new Position(x, y));
                addTile(new Position(x, y));
            }
        }
    }

    private IBoardEntity get(int x, int y) {
        if (board.ContainsKey(new Position(x, y)))
            return board[new Position(x, y)];
        else 
            return null;
    }

    private void addTile(Position position) {
        GameObject tile = Instantiate(this.tile);
        tile.transform.SetParent(tileHolder.transform);
        tile.transform.localPosition = positionToVector(position);
        tile.GetComponent<OnHover>().enter += () => previewIngredient(selectedIngredient, position);
        tile.GetComponent<OnHover>().exit += () => clearIngredientPreview();
        tile.GetComponent<OnHover>().click += () => placeIngredient(selectedIngredient, position);
        tiles.Add(tile.GetComponent<TileOnHover>());
    }

    private IBoardEntity addBoardEntity(IBoardEntity boardEntity, Position position) {
        if (!board.ContainsKey(position)) {
            GameObject newGem = Instantiate(boardEntity.gameObject);
            newGem.transform.SetParent(bEHolder.transform, false);
            newGem.transform.localPosition = positionToVector(position);
            newGem.GetComponent<IBoardEntity>().setPosition(position);
            newGem.GetComponent<IBoardEntity>().StartBE(positionToLocalVector);
            board.Add(position, newGem.GetComponent<IBoardEntity>());
            return newGem.GetComponent<IBoardEntity>();
        } else {
            return null;
        }
    }
    
    private void removeRandom() {
        removeBoardEntity(new Position(UnityEngine.Random.Range(0, width - 1), UnityEngine.Random.Range(0, height - 1)));
        checkFalling();
    }

    public IBoardEntity removeBoardEntity (Position position) {
        if (board.ContainsKey(position)) {
            IBoardEntity boardEntity = board[position];
            board.Remove(position);
            boardEntity.GetComponent<FadeOnDestroy>().Destroy(.15f);
            return boardEntity;
        } else {
            return null;
        }
    }

    private float moveBoardEntity(IBoardEntity boardEntity, Position position, bool remove = true, bool immediate = false, float fallingSpeed = fallingSpeed) {
        if (board.ContainsKey(position)) {
            return -1f;
        }
        if (remove) {
            board.Remove(boardEntity.position);
        }
        board.Add(position, boardEntity);
        boardEntity.setPosition(position);
        if (immediate) {
            boardEntity.lerpToTimed(positionToVector(boardEntity.position), 0);
            return 0;
        }
        return boardEntity.lerpTo(positionToVector(boardEntity.position), fallingSpeed);
    }

    private float moveBoardEntities (Dictionary<Position, Position> displacements) {
        Dictionary<Position, IBoardEntity> boardEntitiesRemoved = displacements.Keys.Select(pos => board[pos])
          .ToDictionary(be => be.position, be => be);

        foreach (Position position in displacements.Keys) {
            board.Remove(position);
        }

        foreach (Position position in displacements.Keys) {
            moveBoardEntity(boardEntitiesRemoved[position], position + displacements[position], false, true);
        }
        return 0;
    }

    public float checkFalling() {
        bool falling = false;
        float fallingTime = 0;

        for (int x = 0; x < width; x++) {
            List<int> dropBy = new List<int>();
            dropBy.Add(0);
            List<IBoardEntity> addedBoardEntities = new List<IBoardEntity>();// = addBoardEntity()

            for (int y = 0; y < height; y++) {
                Position checkPosition = new Position(x, y);
                if (!board.ContainsKey(checkPosition)) {
                    falling = true;
                    dropBy.Add(dropBy[dropBy.Count-1] +1);
                    addedBoardEntities.Add(addBoardEntity(
                        availibleGems[UnityEngine.Random.Range(0, availibleGems.Count)], 
                        new Position(x, height - 1 + dropBy[dropBy.Count - 1])));

                } else {
                    dropBy.Add(dropBy[dropBy.Count - 1]);
                }
            }

            for (int y = 0; y < height; y++) {
                Position checkPosition = new Position(x, y);
                if (board.ContainsKey(checkPosition)) {
                    fallingTime = Math.Max(fallingTime, moveBoardEntity(board[checkPosition], new Position(x, y - dropBy[y])));
                }
            }

            foreach (IBoardEntity currentAddedBordEntity in addedBoardEntities){
                fallingTime = Math.Max(fallingTime, moveBoardEntity(currentAddedBordEntity, currentAddedBordEntity.position + new Position(0, -dropBy[dropBy.Count - 1])));
            }

        }

        if (falling) {
            StartCoroutine(checkMatchesAfter(fallingTime + .5f));
        }

        return 0;
    }

    IEnumerator checkMatchesAfter(float time) {
        checkingMatchCoroutines++;
        yield return new WaitForSeconds(time);
        if (checkingMatchCoroutines > 0) {
            checkingMatchCoroutines--;                
        }
        checkMatches();
    }

    private Boolean checkingMatches() {
        return checkingMatchCoroutines != 0;
    }

    private void checkMatches() {
        List<Match> matches = getMatches();
        foreach (Match match in matches) {
            foreach (IBoardEntity be in match.boardEntities) {
                removeBoardEntity(be.position);
            }
        }
        if (matches.Count > 0) {
            int currentScore = matches.Aggregate(0, (acc, match) =>  acc + match.getScoreValue());
            if (sendToCustomer) {
                FindObjectOfType<GemScore>().AddScore(currentScore);
            }

            FindObjectOfType<SoundEffectPlayer>().PlaySoundEffect(scoreSound, .5f);

            int scoreValue = 0;
            if (currentScore == 1) {
                scoreValue = 10;
            } else if (currentScore == 2) {
                scoreValue = 25;
            } else if (currentScore >= 3) {
                scoreValue = 100;
            }
            score.addScore(scoreValue);

            GameObject scoreFade = Instantiate(scoreFadePF);
            scoreFade.transform.position = matches[0].boardEntities.First().transform.position + new Vector3(0,0,-9);
            scoreFade.GetComponent<ScoreFade>().setScore(scoreValue);
            currentMatches.AddRange(matches);

            checkFalling();
        } else {
            foreach (TileOnHover tile in tiles) {
                tile.checkEntered();
            }
            int score = currentMatches.Aggregate(0, (prod, next) => prod + next.getScoreValue());
            if (sendToCustomer) {
                Core.core.ExecuteAfterTime(1.0f, () => FindObjectOfType<GemScore>().SendScoreToCustomer());
                //FindObjectOfType<CustomerManager>().ScoreNextCustomer(score);
            }
            currentMatches.Clear();
        }
    }

    private List<Match> getMatches() {
        List<Match> matches = new List<Match>();
        
        //check cols
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height - 2; y++) {
                IBoardEntity be1 = get(x, y);
                IBoardEntity be2 = get(x, y+1);
                IBoardEntity be3 = get(x, y+2);
                if( be1.matchable && be2.matchable && be3.matchable
                    && be1.getType() == be2.getType() && be2.getType() == be3.getType()) {
                    matches.Add(new Match(new HashSet<IBoardEntity>() { be1, be2, be3 }));
                }
            }
        }

        //check rows
        for (int y = 0; y < height; y++) {
            for (int x = 0; x < width - 2; x++) {
                IBoardEntity be1 = get(x, y);
                IBoardEntity be2 = get(x+1, y);
                IBoardEntity be3 = get(x+2, y);
                if (be1.matchable && be2.matchable && be3.matchable
                    && be1.getType() == be2.getType() && be2.getType() == be3.getType()) {
                    matches.Add(new Match(new HashSet<IBoardEntity>() { be1, be2, be3 }));
                }
            }
        }

        List<Match> combinedMatches = new List<Match>();
        ISet<Match> usedMatches = new HashSet<Match>();
        foreach (Match match1 in matches) {
            foreach (Match match2 in matches) {
                if (match1 == match2 || usedMatches.Contains(match1) || usedMatches.Contains(match2)) {
                    continue;
                }
                if (match1.boardEntities.Overlaps(match2.boardEntities)) {
                    HashSet<IBoardEntity> combindedHashSet = new HashSet<IBoardEntity>(match1.boardEntities);
                    combindedHashSet.UnionWith(match2.boardEntities);
                    usedMatches.Add(match1);
                    usedMatches.Add(match2);
                    combinedMatches.Add(new Match(combindedHashSet));
                }
            }
        }
        List<Match> newMatches = new List<Match>();
        foreach (Match match in matches) {
            if (!usedMatches.Contains(match)) {
                newMatches.Add(match);
            }
        }
        newMatches.AddRange(combinedMatches);


        return newMatches;
    }

    public void placeIngredient(Ingredient ingredient, Position position) {
        clearIngredientPreview();
        if (!canUseIngredient(ingredient, position)) {
            return;
        }
        sendToCustomer = true;

        realDisplacements(ingredient.getDisplacements(position));

        foreach (KeyValuePair<Position, IBoardEntity> entry in ingredient.boardEntities) {
            addBoardEntity(entry.Value, entry.Key + position);
        }
        
        checkBoardEntitiesOutofBounds();
        StartCoroutine(checkMatchesAfter(1));
    }

    public void previewIngredient(Ingredient ingredient, Position position) {
        clearPreviewDisplacement();
        if (!canUseIngredient(ingredient, position)) {
            return;
        }     

        foreach (IBoardEntity oldBoardEntity in previewBoardEntities) {
            Destroy(oldBoardEntity.gameObject);
        }
        previewBoardEntities.Clear();


        foreach (KeyValuePair<Position, IBoardEntity> boardEntity in ingredient.boardEntities) {
            GameObject gameObject = Instantiate(boardEntity.Value.gameObject);
            IBoardEntity be = gameObject.GetComponent<IBoardEntity>();
            gameObject.GetComponent<Ghostable>().ghostOn();
            previewBoardEntities.Add(be);

            be.transform.SetParent(bEHolder.transform, false);
            be.transform.localPosition = positionToVector(position + boardEntity.Key);

        }
        previewDisplacements(ingredient.getDisplacements(position));
    }

    public Boolean canUseIngredient(Ingredient ingredient, Position position) {
        if (checkingMatchCoroutines != 0) {
            return false;
        }
        foreach (Position position2 in ingredient.boardEntities.Keys) {
            if (!positionInBoard(position + position2)) {
                return false;
            }
        }
        return true;
    }

    public void clearIngredientPreview() {
        clearPreviewDisplacement();

        foreach (IBoardEntity oldBoardEntity in previewBoardEntities) {
            Destroy(oldBoardEntity.gameObject);
        }
        previewBoardEntities.Clear();

    }

    private void previewDisplacements(Dictionary<Position, Position> displacements) {
        Dictionary<Position, Position> calculatedDisplacements = calculateDisplacements(displacements);
        doPreviewDisplacement(calculatedDisplacements);
    }

    private void doPreviewDisplacement(Dictionary<Position, Position> displacements) {
        foreach (KeyValuePair<Position, Position> displacement in displacements) {
            board[displacement.Key].setDisplacementTimed(displacement.Value, displaceTime);
        }
    }
    
    private void realDisplacements(Dictionary<Position, Position> displacements) {
        Dictionary<Position, Position> calculatedDisplacements = calculateDisplacements(displacements);
        doRealDisplacement(calculatedDisplacements);
    }

    private void doRealDisplacement(Dictionary<Position, Position> displacements) {
        moveBoardEntities(displacements);
    }

    private Dictionary<Position, Position> calculateDisplacements(Dictionary<Position, Position> displacements) {
        Dictionary<Position, Position> positionToDisplacement = new Dictionary<Position, Position>();

        foreach (KeyValuePair<Position, Position> displacement in displacements) {
            Position displacementNormalized = displacement.Value.getNormalized();
            Position position = displacement.Key;

            Position goToPosition = new Position(position.x, position.y);
            Position goToNormalized = displacementNormalized * -1;
            Position goToNext = new Position(position.x, position.y) + goToNormalized;
            while(positionInBoard(goToNext) && displacements.ContainsKey(goToNext)) {
                goToPosition += goToNormalized;
                goToNext += goToNormalized;
            }
            position = goToPosition;

            while (positionInBoard(position)) {
                if (positionToDisplacement.ContainsKey(position)) {
                    positionToDisplacement[position] = positionToDisplacement[position] + displacementNormalized;
                } else {
                    positionToDisplacement[position] = displacementNormalized;
                }
                position = position + displacementNormalized;
            }
        }

        return positionToDisplacement;
    }
    

    public void clearPreviewDisplacement() {
        foreach (IBoardEntity be in board.Values) {
            be.clearDisplacementTimed(displaceTime);
        }
    }

    public void checkBoardEntitiesOutofBounds() {
        foreach (KeyValuePair<Position, IBoardEntity> entry in board) {
            if (!positionInBoard(entry.Key)) {
                Destroy(entry.Value.gameObject);
            }
        }
        board = board.Where(i => positionInBoard(i.Key))
         .ToDictionary(i => i.Key, i => i.Value);
    }

  

    private bool positionInBoard(Position position) {
        if (position == null) {
            return false;
        } else {
            return position.x >= 0 && position.y >= 0 && position.x < width && position.y < height;
        }

    }

    private Vector3 positionToVector (Position position) {
        return new Vector3(position.x * gemWidth - (gemWidth * width /2 - gemWidth/2), position.y * gemHeight);
    }

    private Vector3 positionToLocalVector(Position position) {
        return new Vector3(position.x * gemWidth, position.y * gemHeight);
    }
}
