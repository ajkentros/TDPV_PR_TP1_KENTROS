using UnityEngine;

public class Fruit : MonoBehaviour
{
    [SerializeField] private BoxCollider2D areaPlay;           // Delimita el area dónde se instanciará la fruit
    [SerializeField] private LayerMask obstacleLayer;          // Capa donde están los obstáculos

    // Start is called before the first frame update
    void Start()
    {
        PositionFruit();
    }

    public void PositionFruit()
    {
        /*
         * Crea una lista con todos los GameObject con etiqueta Obstacle
         * define un área bounds = al área de juego
         * Define una posición futura para la fruit
         * Generar una posición aleatoria dentro del área de juego con un número aleatorio positionFruitX que tenga como rango = a los extremos del área bounds en x
         * calcula un número aleatorio positionFruitY que tenga como rango = a los extremos del área bounds en y
         * Verificar¡ si la posición generada intercepta con algún obstáculo
         * Posiciona la fruit a partir de los números obtenidos
         */

       
        Bounds areaBounds = this.areaPlay.bounds;
        Vector2 positionFruit;

        do
        {
            // Generar una posición aleatoria dentro del área de juego
            float positionFruitX = Random.Range(areaBounds.min.x, areaBounds.max.x);
            float positionFruitY = Random.Range(areaBounds.min.y, areaBounds.max.y);
            positionFruit = new Vector2(Mathf.Round(positionFruitX), Mathf.Round(positionFruitY));

            // Verificar si la posición generada intercepta con algún obstáculo
        } while (IntersectsObstacle(positionFruit));

        this.transform.position = positionFruit;

    }

    // Verifica si la posición de la fruta intercepta con algún obstáculo
    private bool IntersectsObstacle(Vector2 fruitPosition)
    {
        /*
         * Obtiene todos los colliders en la posición de la fruta
         * Si hay algún collider en la posición de la fruta => significa que está intersectando con un obstáculo
         */

        Collider2D[] colliders = Physics2D.OverlapPointAll(fruitPosition, obstacleLayer);
        
        return colliders.Length > 0;
    }

    // Gestiona la posición aleatoria de la fruit
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Si la fruit colisiona con la snake, se vuelve a crear en otra posición del área de juego
        if (other.CompareTag("Snake"))
        {
            PositionFruit();
        }
    }
}
