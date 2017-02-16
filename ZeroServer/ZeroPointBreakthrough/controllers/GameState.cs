using System;
public class GameState
{
    private int players;
    private string[] map;

    public GameState() {
        players = 0;
        map = new string[10];
    }

    public void setPlayerAmount(int count){
        this.players = count;
    }

    public void setMap(string[] mapToBe){
        this.map = mapToBe;
    }

    public string getMap(){
        string returnable = "";
        for (int i = 0; i < this.map.Length - 1; i++)
        {
            returnable = returnable + this.map[i];
        }
        return returnable;
    }
    public string getPlayerCount(){
        return players.ToString();
    }
}