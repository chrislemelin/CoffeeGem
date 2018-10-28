using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Match {

    public HashSet<IBoardEntity> boardEntities { get; }

    public Match(HashSet<IBoardEntity> boardEntities) {
        this.boardEntities = boardEntities;
    }

    public MatchScore getScore() {
        if (boardEntities.Count <= 3) {
            return MatchScore.one;
        } else if (boardEntities.Count == 4) {
            return MatchScore.two;
        } else {
            return MatchScore.three;
        }

    }
    public int getScoreValue() {
        if (boardEntities.Count <= 3) {
            return 1;
        } else if (boardEntities.Count == 4) {
            return 2;
        } else {
            return 3;
        }

    }

}

public enum MatchScore { one, two, three }
