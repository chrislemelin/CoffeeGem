using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MatchCalculator : MonoBehaviour {

    static public MatchCalculator matchCalculator;

    void Awake() {
        matchCalculator = this;
    }

    public List<BoardMatch> getMatches(Dictionary<Position, IBoardEntity> board, int width, int height) {
        List<BoardMatch> matches = new List<BoardMatch>();

        //check cols
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height - 2; y++) {
                IBoardEntity be1 = get(board, x, y);
                IBoardEntity be2 = get(board, x, y + 1);
                IBoardEntity be3 = get(board, x, y + 2);
                if (be1 != null && be2 != null && be3 != null &&
                    be1.getMatchable() && be2.getMatchable() && be3.getMatchable()
                    && be1.getType() == be2.getType() && be2.getType() == be3.getType()) {
                    matches.Add(new BoardMatch(new List<IBoardEntity>() { be1, be2, be3 }, be1.getType()));
                }
            }
        }

        //check rows
        for (int y = 0; y < height; y++) {
            for (int x = 0; x < width - 2; x++) {
                IBoardEntity be1 = get(board, x,y);
                IBoardEntity be2 = get(board, x + 1, y);
                IBoardEntity be3 = get(board, x + 2, y);
                if (be1 != null && be2 != null && be3 != null &&
                    be1.getMatchable() && be2.getMatchable() && be3.getMatchable()
                    && be1.getType() == be2.getType() && be2.getType() == be3.getType()) {
                    matches.Add(new BoardMatch(new List<IBoardEntity>() { be1, be2, be3 }, be1.getType()));
                }
            }
        }

        List<BoardMatch> combinedMatches = new List<BoardMatch>();
        ISet<BoardMatch> usedMatches = new HashSet<BoardMatch>();
        foreach (BoardMatch match1 in matches) {
            foreach (BoardMatch match2 in matches) {
                if (match1 == match2 || usedMatches.Contains(match1) || usedMatches.Contains(match2)) {
                    continue;
                }
                if (match1.boardEntities.Intersect(match2.boardEntities).Count() > 0) {
                    List<IBoardEntity> combindedHashSet = new List<IBoardEntity>(match1.boardEntities);
                    combindedHashSet.AddRange(match2.boardEntities);
                    usedMatches.Add(match1);
                    usedMatches.Add(match2);
                    combinedMatches.Add(new BoardMatch(combindedHashSet));
                }
            }
        }
        List<BoardMatch> newMatches = new List<BoardMatch>();
        foreach (BoardMatch match in matches) {
            if (!usedMatches.Contains(match)) {
                newMatches.Add(match);
            }
        }
        newMatches.AddRange(combinedMatches);

        return newMatches;
    }

    private IBoardEntity get(Dictionary<Position, IBoardEntity> board, int x, int y) {
        if (board.ContainsKey(new Position(x, y)))
            return board[new Position(x, y)];
        else
            return null;
    }
}
