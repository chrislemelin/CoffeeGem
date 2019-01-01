using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Match {

    public MatchScore score { get; }
    public GemType type { get; }
    public Position position { get; }
    public IToMatch source { get; }

    public Match(MatchScore score, GemType type, Position position, IToMatch source) {
        this.type = type;
        this.score = score;
        this.position = position;
        this.source = source;
    }

    public int getScoreValue() {
        return (int)score;
    }

}

public enum MatchScore { one = 1, two = 2, three = 3 }
