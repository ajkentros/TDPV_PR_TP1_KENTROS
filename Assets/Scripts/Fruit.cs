using UnityEngine;

public class Fruit : MonoBehaviour
{
    [SerializeField] private BoxCollider2D areaPlay;           // Delimita el area d�nde se instanciar� la fruit
    [SerializeField] private LayerMask obstacleLayer;          // Capa donde est�n los obst�culos

    // Start is called before the first frame update
    void Start()
    {
        PositionFruit();
    }

    public void PositionFruit()
    {
        /*
         * Crea una lista con todos los GameObject con etiqueta Obstacle
         * define un �rea bounds = al �rea de juego
         * Define una posici�n futura para la fruit
         * Generar una posici�n aleatoria dentro del �rea de juego con un n�mero aleatorio positionFruitX que tenga como rango = a los extremos del �rea bounds en x
         * calcula un n�mero aleatorio positionFruitY que tenga como rango = a los extremos del �rea bounds en y
         * Verificar� si la posici�n generada intercepta con alg�n obst�culo
         * Posiciona la fruit a partir de los n�meros obtenidos
         */

       
        Bounds areaBounds = this.areaPlay.bounds;
        Vector2 positionFruit;

        do
        {
            // Generar una posici�n aleatoria dentro del �rea de juego
            float positionFruitX = Random.Range(areaBounds.min.x, areaBounds.max.x);
            float positionFruitY = Random.Range(areaBounds.min.y, areaBounds.max.y);
            positionFruit = new Vector2(Mathf.Round(positionFruitX), Mathf.Round(positionFruitY));

            // Verificar si la posici�n generada intercepta con alg�n obst�culo
        } while (IntersectsObstacle(positionFruit));

        this.transform.position = positionFruit;

    }

    // Verifica si la posici�n de la fruta intercepta con alg�n obst�culo
    private bool IntersectsObstacle(Vector2 fruitPosition)
    {
        /*
         * Obtiene todos los colliders en la posici�n de la fruta
         * Si hay alg�n collider en la posici�n de la fruta => significa que est� intersectando con un obst�culo
         */

        Collider2D[] colliders = Physics2D.OverlapPointAll(fruitPosition, obstacleLayer);
        
        return colliders.Length > 0;
    }

    // Gestiona la posici�n aleatoria de la fruit
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Si la fruit colisiona con la snake, se vuelve a crear en otra posici�n del �rea de juego
        if (other.CompareTag("Snake"))
        {
            PositionFruit();
        }
    }
}
