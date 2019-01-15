using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Board : BoardPlacedIngredientManager {

    [SerializeField]
    private float boardWidth;

    [SerializeField]
    private int height = 5;
    [SerializeField]
    private int width = 6;
    [SerializeField]
    private List<IBoardEntity> availibleGems = new List<IBoardEntity>();
    [SerializeField]
    private GameObject tilePf;
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
    private Color highlightColor;
    [SerializeField]
    private Color adjacentColor;
    [SerializeField]
    private GameObject scorePipPF;

    [SerializeField]
    private GameObject scoreFadePF;

    [SerializeField]
    private AudioClip scoreSound;

    private bool init = false;
    private bool started = false;
    private bool boardLocked = false;
    private float gemHeight;
    private float gemWidth;
    private int checkingMatchCoroutines = 0;
    private float beScalar;

    private List<IBoardEntity> previewBoardEntities = new List<IBoardEntity>();
    private List<TileOnHover> tiles = new List<TileOnHover>();
    private Dictionary<Position, Tile> positionToTile = new Dictionary<Position, Tile>();
    private Ingredient selectedIngredient;
    private IngredientSelector ingredientSelector;
    private MatchCalculator matchCalculator;
    private BELibrary bELibrary;

    private Dictionary<Position, IBoardEntity> board = new Dictionary<Position, IBoardEntity>();
    private List<BoardMatch> currentBoardMatches = new List<BoardMatch>();
    private List<IToMatch> currentMatches = new List<IToMatch>();

    // Use this for initialization
    void Start () {
        ingredientSelector = FindObjectOfType<IngredientSelector>();
        matchCalculator = MatchCalculator.matchCalculator;
        float targetGemWidth = boardWidth / width;
        bELibrary = LibraryManager.instance.get<BELibrary>();

        RectTransform rt = (RectTransform)availibleGems[0].gameObject.transform;
        float tempGemWidth = rt.rect.width;
        beScalar = targetGemWidth / tempGemWidth;
        foreach (IBoardEntity be in availibleGems) {
            be.gameObject.transform.localScale = new Vector2(beScalar, beScalar);
        }

        gemWidth = targetGemWidth;
        gemHeight = targetGemWidth;

        selectedIngredient = null;
        DayManager.dayManager.start += () => {
            populateBoard();
            unlockBoard();
            //checkMatchesAfterHelper(1);
        };
        DayManager.dayManager.end += () => {
            clearBoard();
            started = false;          
        };
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
        if (!init) {
            init = true;
            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    //addBoardEntity(availibleGems[UnityEngine.Random.Range(0, availibleGems.Count)], new Position(x, y));
                    addTile(new Position(x, y));
                }
            }
        }
        checkFalling();
    }

    public IBoardEntity get(Position position) {
        return get(position.x, position.y);
    }

    public IBoardEntity get(int x, int y) {
        if (board.ContainsKey(new Position(x, y)))
            return board[new Position(x, y)];
        else 
            return null;
    }

    private void addTile(Position position) {
        GameObject tile = Instantiate(this.tilePf);
        tile.transform.SetParent(tileHolder.transform);
        tile.transform.localPosition = positionToVector(position);
        tile.transform.localScale = new Vector3(beScalar, beScalar, 1);
        tile.GetComponent<OnEvent>().enter += () => {
            previewIngredient(selectedIngredient, position);
            bool canPlace = canUseIngredient(selectedIngredient, position);
            if (!canPlace) {
                tile.GetComponent<ColorLerp>().lerpToColor(Color.red);
            } else {
                tile.GetComponent<ColorLerp>().lerpToColor(Color.white);
            }
        };

        tile.GetComponent<OnEvent>().exit += () => clearIngredientPreview();
        tile.GetComponent<OnEvent>().click += () => placeIngredient(selectedIngredient, position);
        tiles.Add(tile.GetComponent<TileOnHover>());
        positionToTile.Add(position, tile.GetComponent<Tile>());
    }

    private IBoardEntity addBoardEntity(IBoardEntity boardEntity, Position position, bool show = true) {
        if (!board.ContainsKey(position)) {
            GameObject newGem = Instantiate(boardEntity.gameObject);
            newGem.transform.SetParent(bEHolder.transform, false);
            newGem.transform.localPosition = positionToVector(position);
            newGem.GetComponent<IBoardEntity>().setPosition(position);
            newGem.GetComponent<IBoardEntity>().StartBE(positionToLocalVector);
            newGem.transform.localScale = new Vector2(beScalar, beScalar);
            newGem.GetComponent<Fade>().setShow(show, true);
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

    public IBoardEntity removeBoardEntitySlow(Position position) {
        if (board.ContainsKey(position)) {
            IBoardEntity boardEntity = board[position];
            boardEntity.GetComponent<FadeOnDestroy>().setDuration(2.0f);
            return removeBoardEntity(position);
        } else {
            return null;
        }
    }

    public IBoardEntity removeBoardEntity(Position position) {
        if (board.ContainsKey(position)) {
            IBoardEntity boardEntity = board[position];
            board.Remove(position);
            boardEntity.GetComponent<FadeOnDestroy>().Destroy(.25f);
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

    public bool checkFalling() {
        bool falling = false;
        float fallingTime = 0;

        for (int x = 0; x < width; x++) {
            List<int> dropBy = new List<int>();
            dropBy.Add(0);
            List<IBoardEntity> addedBoardEntities = new List<IBoardEntity>();

            for (int y = 0; y < height; y++) {
                Position checkPosition = new Position(x, y);
                if (!board.ContainsKey(checkPosition)) {
                    falling = true;
                    dropBy.Add(dropBy[dropBy.Count - 1] + 1);
                    IBoardEntity be;
                    if (getPlacedIngredientFallingTypes().Count != 0) {
                        be = LibraryManager.instance.get<BELibrary>().get(getPlacedIngredientFallingTypes()[0]);
                        getPlacedIngredientFallingTypes().RemoveAt(0);
                    } else {
                        be = availibleGems[UnityEngine.Random.Range(0, availibleGems.Count)];
                    }

                    addedBoardEntities.Add(addBoardEntity(be, new Position(x, height - 1 + dropBy[dropBy.Count - 1]), false));

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

            foreach (IBoardEntity currentAddedBordEntity in addedBoardEntities) {
                Position oldPosition = currentAddedBordEntity.position;
                fallingTime = Math.Max(fallingTime, moveBoardEntity(currentAddedBordEntity, currentAddedBordEntity.position + new Position(0, -dropBy[dropBy.Count - 1])));
                if (!positionInBoard(oldPosition)) {
                    float dropRatio = ((float)oldPosition.y - height) / dropBy[dropBy.Count - 1];
                    Core.core.ExecuteAfterTime(fallingTime * dropRatio, () => {
                        currentAddedBordEntity.GetComponent<FadeOnDestroy>().setDuration(fallingTime / dropBy[dropBy.Count - 1] * 2 / 3 );
                        currentAddedBordEntity.GetComponent<Fade>().setShow(true);
                    });
                }
            }

        }
        if (falling) {
            checkMatchesAfterHelper(fallingTime + .45f, sendScoreToCustomer: placedIngredient != null ? !placedIngredient.bonusPlacement : false);
        }
        return falling;
    }

    public void checkMatchesAfterHelper(float time, bool forceCheckFalling = false, bool sendScoreToCustomer = true) {
        StartCoroutine(checkMatchesAfter(time, forceCheckFalling, sendScoreToCustomer));
    }

    IEnumerator checkMatchesAfter(float time, bool forceCheckFalling = false, bool sendScoreToCustomer = true) {
        checkingMatchCoroutines++;
        yield return new WaitForSeconds(time);
        if (checkingMatchCoroutines > 0) {
            checkingMatchCoroutines--;                
        }
        checkMatches(forceCheckFalling, sendScoreToCustomer);
    }

    public void unlockBoard() {
        boardLocked = false;
        foreach (TileOnHover tile in tiles) {
            tile.checkEntered();
        }      
    }

    public void clearBoard() {
        List<Position> positions = board.Keys.ToList();
        foreach (Position position in positions) {
            removeBoardEntitySlow(position);
        }
    }

    private void addToCurrentMatches(IEnumerable<IToMatch> toMathces, bool recordMatches = true) {
        //int currentScore = boardMatches.Aggregate(0, (acc, match) => acc + match.getScoreValue());
        FindObjectOfType<SoundEffectPlayer>().PlaySoundEffect(scoreSound, .5f);
        if (recordMatches) {
            List<Match> matches = toMathces.Select((match) => match.toMatch()).ToList();
            if (started) {
                FindObjectOfType<GemScore>().AddMatches(matches, this);
            }
        }

  
        // currentMatches.AddRange(toMathces);
        //    this is the old scoring system
        //  int scoreValue = 0;
        //    if (currentScore == 1) {
        //        scoreValue = 10;
        //    } else if (currentScore == 2) {
        //        scoreValue = 25;
        //    } else if (currentScore >= 3) {
        //        scoreValue = 100;
        //    }
        //    score.addScore(scoreValue);

        //GameObject scoreFade = Instantiate(scoreFadePF);
        //scoreFade.transform.position = boardMatches[0].boardEntities.First().transform.position + new Vector3(0, 0, -9);
        //scoreFade.GetComponent<ScoreFade>().setScore(scoreValue);
        //currentBoardMatches.AddRange(boardMatches);       
    }

    private void checkMatches(bool forceCheckFalling = false, bool sendScoreToCustomer = true) {
        List<BoardMatch> boardMatches = matchCalculator.getMatches(board, width, height);
        sendScoreToCustomer = sendScoreToCustomer && started;
       
        if (boardMatches.Count > 0) {
            addToCurrentMatches(boardMatches, started);

            foreach (BoardMatch boardMatch in boardMatches) {
                foreach (IBoardEntity be in boardMatch.boardEntities) {
                    removeBoardEntity(be.position);
                }
            }
            checkFalling();
        } 
        else {
            if ((forceCheckFalling && !checkFalling()) || !forceCheckFalling) {
                int score = currentBoardMatches.Aggregate(0, (prod, next) => prod + next.getScoreValue());
                getPlacedIngredientFallingTypes().Clear();
                if (sendScoreToCustomer) {
                    boardLocked = true;
                    Core.core.ExecuteAfterTime(1.0f, () => FindObjectOfType<GemScore>().SendScoreToCustomer());
                } else {
                    foreach (TileOnHover tile in tiles) {
                        tile.checkEntered();
                    }
                }
                currentBoardMatches.Clear();
            }

            foreach (BoardMatch match in boardMatches) {
                foreach (IBoardEntity be in match.boardEntities) {
                    removeBoardEntity(be.position);
                }
            }
        }
        
    }   

    public void placeIngredient(Ingredient ingredient, Position position) {
        if (!canUseIngredient(ingredient, position)) {
            return;
        }
        clearIngredientPreview();
        started = true;
        placedIngredient = ingredient;

        ingredient.initIngredient(removeGems, changeGems);
        realDisplacements(ingredient.getDisplacements(position));

        foreach (KeyValuePair<Position, IBoardEntity> entry in ingredient.boardEntities) {
            addBoardEntity(entry.Value, entry.Key + position);
        }

        List<IBoardEntity> removedGems = checkBoardEntitiesOutofBounds();
        ingredient.boardEntitiesPushed(removedGems);

        ingredient.ingredientPlaced(board, position);
        addToCurrentMatches(getPlacedIngredientMatches(position));
        ingredientSelector.ingredientUsed(ingredient);
        checkMatchesAfterHelper(.5f, true, !getPlacedIngredientBonusPlacement());
    }

    public void previewIngredient(Ingredient ingredient, Position position) {
        clearPreviewDisplacement();
        if (!canPreviewIngredient(ingredient, position)) {
            return;
        }     

        foreach (IBoardEntity oldBoardEntity in previewBoardEntities) {
            Destroy(oldBoardEntity.gameObject);
        }
        previewBoardEntities.Clear();

        foreach (KeyValuePair<Position, IBoardEntity> boardEntity in ingredient.boardEntities) {
            GameObject gameObject = Instantiate(boardEntity.Value.gameObject);
            gameObject.transform.localScale = new Vector3(beScalar, beScalar, 1);
            IBoardEntity be = gameObject.GetComponent<IBoardEntity>();
            previewBoardEntities.Add(be);

            be.transform.SetParent(bEHolder.transform, false);
            be.transform.localPosition = positionToVector(position + boardEntity.Key);

            if (!positionInBoard(boardEntity.Key + position)) {
                GameObject tile = Instantiate(tilePf);
                //tile.GetComponent<Fade>().setShow(true);
                tile.transform.SetParent(be.transform, false);
                tile.transform.localPosition = new Vector3(0,0,-.75f);

                tile.GetComponent<Tile>().background.GetComponent<ColorLerp>().lerpToColor(Color.red);
                tile.GetComponent<Tile>().background.GetComponent<Fade>().setShow(true);
            } else {
                positionToTile[boardEntity.Key + position].background.GetComponent<ColorLerp>().lerpToColor(highlightColor);
                positionToTile[boardEntity.Key + position].background.GetComponent<Fade>().setShow(true, true);
            }

            if (ingredient.showAdjacent) {
                foreach (Position adjacentPosition in ingredient.getAdjacentPositions()) {
                    Position tempPosition = adjacentPosition + position;
                    if (positionInBoard(tempPosition)) {
                        positionToTile[adjacentPosition + position].background.GetComponent<ColorLerp>().lerpToColor(adjacentColor);
                        positionToTile[adjacentPosition + position].background.GetComponent<Fade>().setShow(true, true);
                    }
                }
            }


        }
        previewDisplacements(ingredient.getDisplacements(position));
    }

    public bool canUseIngredient(Ingredient ingredient, Position position) {
        if (!canPreviewIngredient(ingredient, position)) {
            return false;
        }
        foreach (Position position2 in ingredient.boardEntities.Keys) {
            if (!positionInBoard(position + position2)) {
                return false;
            }
        }
        return true;
    }

    public void removeGems(ISet<Position> gemsToRemove) {
        foreach (Position position in gemsToRemove) {
            removeBoardEntity(position);
        }
    }

    public void changeGems(Dictionary<Position, GemType> changeGems) {
        removeGems(new HashSet<Position>(changeGems.Keys.ToList()));
        foreach (KeyValuePair<Position, GemType> entry in changeGems) {
            addBoardEntity(bELibrary.get(entry.Value), entry.Key);
        }
    }

    public bool canPreviewIngredient(Ingredient ingredient, Position position) {
        if (checkingMatchCoroutines != 0) {
            return false;
        }
        if (boardLocked) {
            return false;
        }
        return true;
    }

    public void clearIngredientPreview() {
        clearPreviewDisplacement();

        foreach (Tile tile in positionToTile.Values) {
            tile.background.GetComponent<Fade>().setShow(false, true);
        }

        foreach (IBoardEntity boardEntity in board.Values) {
            boardEntity.GetComponent<Fade>().setTransparent(1);
        }

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
            if (!positionInBoard(displacement.Key + displacement.Value)) {
                board[displacement.Key].GetComponent<Fade>().setTransparent(.5f);
            }
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

    public List<IBoardEntity> checkBoardEntitiesOutofBounds() {
        List<IBoardEntity> removedGems = new List<IBoardEntity>();
        foreach (KeyValuePair<Position, IBoardEntity> entry in board) {
            if (!positionInBoard(entry.Key)) {
                removedGems.Add(entry.Value);
                Destroy(entry.Value.gameObject);
            }
        }
        board = board.Where(i => positionInBoard(i.Key))
         .ToDictionary(i => i.Key, i => i.Value);
        return removedGems;
    }


    private bool positionInBoard(Position position) {
        if (position == null) {
            return false;
        } else {
            return position.x >= 0 && position.y >= 0 && position.x < width && position.y < height;
        }
    }

    public Vector3 positionToVector (Position position) {
        return new Vector3(position.x * gemWidth - (gemWidth * width /2 - gemWidth/2), position.y * gemHeight + (gemHeight / 2));
    }

    public Vector3 positionToLocalVector(Position position) {
        return new Vector3(position.x * gemWidth, position.y * gemHeight) / beScalar;
    }
}
