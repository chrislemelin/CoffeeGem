using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientMatch : IToMatch {

    public MatchScore score { get; }
    public GemType type { get; }
    public Position position { get; }

    public IngredientMatch(MatchScore score, GemType type, Position position) {
        this.score = score;
        this.type = type;
        this.position = position;
    }

    public Match toMatch() {
        return new Match(score, type, position, this);
    }


}
