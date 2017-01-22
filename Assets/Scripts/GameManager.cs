//Game manager class to keep record of the points
public class gameManager
{
  //Instance of the class
  private static gameManager instance;

  //Points in the instance
  public int points;

  //Initializes points in 0
  private gameManager()
  {
    points = 0;
  }

  //Defines public instance and creates a new one when necessary
  public static gameManager Instance
  {
    get
    {
      if (instance == null)
      {
        instance = new gameManager();
      }

      return instance;
    }
  }
}