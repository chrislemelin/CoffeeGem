using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardMatch : IToMatch {

    public List<IBoardEntity> boardEntities { get; }
    private GemType type;

    public BoardMatch(List<IBoardEntity> boardEntities, GemType gemType) {
        this.boardEntities = boardEntities;
        type = gemType;
    }

    public BoardMatch(List<IBoardEntity> boardEntities) {
        this.boardEntities = boardEntities;
        type = boardEntities[0].getType() ;
    }

    public Match toMatch() {
        return new Match(getScore(), type, boardEntities[0].position, this);
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
