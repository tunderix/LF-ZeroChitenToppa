using System;

public class GameController
{
    private GameState state;
    private static GameController instance;

    public GameController() {}

    public static GameController Instance
    {
        get 
        {
            if (instance == null)
            {
                instance = new GameController();
            }
            return instance;
        }
    }

   public void setPlayers(int count){
        this.state.setPlayerAmount(count);
   }

   public void initState(){
       this.state = new GameState();
   }

   public GameState getState(){
       return state;
   }

}
