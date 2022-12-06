namespace adventOfCode2022
{
  public class RockPaperScissors
  {
    public enum Move
    {
      ROCK = 1,
      PAPER = 2,
      SCISSORS = 3
    }
    public enum GameResult
    {
      LOSS = 0,
      DRAW = 3,
      WIN = 6
    }
    public Move OpponentMove { get; }
    public Move PlayerMove { get; }
    public RockPaperScissors(Move opponentMove, Move playerMove)
    {
      OpponentMove = opponentMove;
      PlayerMove = playerMove;
    }

    public GameResult Result()
    {
      if (this.OpponentMove == Move.ROCK)
      {
        if (this.PlayerMove == Move.PAPER) return GameResult.WIN;
        if (this.PlayerMove == Move.SCISSORS) return GameResult.LOSS;
      }

      if (this.OpponentMove == Move.PAPER)
      {
        if (this.PlayerMove == Move.ROCK) return GameResult.LOSS;
        if (this.PlayerMove == Move.SCISSORS) return GameResult.WIN;
      }

      if (this.OpponentMove == Move.SCISSORS)
      {
        if (this.PlayerMove == Move.ROCK) return GameResult.WIN;
        if (this.PlayerMove == Move.PAPER) return GameResult.LOSS;
      }

      return GameResult.DRAW;
    }

    public static void GetWinningMove(Move opponentMove, out Move playToWin)
    {
      switch (opponentMove)
      {
        case Move.ROCK:
          playToWin = Move.PAPER;
          break;
        case Move.PAPER:
          playToWin = Move.SCISSORS;
          break;
        case Move.SCISSORS:
          playToWin = Move.ROCK;
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }

    }
    public static void GetDrawingMove(Move opponentMove, out Move playToDraw)
    {
      playToDraw = opponentMove;
    }
    public static void GetLosingMove(Move opponentMove, out Move playToLose)
    {
      switch (opponentMove)
      {
        case Move.ROCK:
          playToLose = Move.SCISSORS;
          break;
        case Move.PAPER:
          playToLose = Move.ROCK;
          break;
        case Move.SCISSORS:
          playToLose = Move.PAPER;
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }

    }


    public int Score()
    {
      return (int)this.Result() + (int)this.PlayerMove;
    }
  }
}