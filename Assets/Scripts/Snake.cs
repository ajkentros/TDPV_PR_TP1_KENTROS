using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Snake : MonoBehaviour
{
    [SerializeField] private int moveSpeedSnake = 1;             // Velocidad de movimiento de la snake
    [SerializeField] private Transform bodySnakePrefab;          // Referencia a el transform de el GameObject BodySnake
    [SerializeField] private GameManager gameManager;            // referencia al GameManager
    [SerializeField] private Fruit fruit;                        // Rferencia a la fruit

    private int initialSizeSnake = 2;
    private List<Transform> bodysList = new();                   // Lista que contendr� las posiciones de los bodys de la snake
    private Vector2 directionSnake = Vector2.up;                 // Vector que indica la direcci�n en que se mueve la snake (inicialmente hacia arriba)
    private bool canChangeDirection = true;                      // Bandera para evitar cambios de direcci�n r�pidos

    private void Start()
    {
        /*
         * instancia la snake en la posici�n y cuerpo inicial
         */
        ResetSnake();
    }


    // Update is called once per frame
    void Update()
    {
        // llama al m�todo que escucha el teclado
        GetImputMove();
    }

    private void FixedUpdate()
    {
        //llama al m�todo que mueva a la snake
        MoveSnake();
    }

    private void MoveSnake()
    {
        /* Versi�n sin time.deltatime
         * Calcula la nueva posici�n de la snake utilizando Mathf.Round para obtener la posici�n entera m�s cercana
         * No se usa Time.deltaTime porque la snake se mueve una unidad de posici�n a la vez, independientemente del tiempo transcurrido
         * Para movimientos discretos es adecuado porque la snake se mueve de una celda a otra en el grid
         * Habilita la capacidad de cambiar de direcci�n
         */

        for(int i = bodysList.Count - 1; i > 0; i--)
        {
            bodysList[i].position = bodysList[i - 1].position;
        }

        float positionSnakeX = Mathf.Round(this.transform.position.x) + directionSnake.x * moveSpeedSnake;
        float positionSnakeY = Mathf.Round(this.transform.position.y) + directionSnake.y * moveSpeedSnake;
        Vector2 newPositionSnake = new(positionSnakeX, positionSnakeY);
        this.transform.position = newPositionSnake;

        canChangeDirection = true;

        ///* Versi�n con time.deltatime
        // * Calcula el desplazamiento basado en la direcci�n de la serpiente y la velocidad
        // * Calcula la nueva posici�n sumando el desplazamiento al la posici�n actual
        // * Asigna la nueva posici�n a la serpiente
        // */
        //Vector2 displacement = directionSnake * moveSpeedSnake * Time.deltaTime;
        //Vector2 newPosition = (Vector2)transform.position + displacement;
        //transform.position = newPosition;

        //canChangeDirection = true;
    }

    // Gestiona el movimieto de la snake seg�n el imput del teclado
    private void GetImputMove()
    {
        // Captura las entradas del teclado para cambiar la direcci�n de la serpiente
        if (canChangeDirection)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && directionSnake != Vector2.down)
            {
                Vector2 up = Vector2.up;
                directionSnake = up;
                canChangeDirection = false;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) && directionSnake != Vector2.up)
            {
                Vector2 down = Vector2.down;
                directionSnake = down;
                canChangeDirection = false;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) && directionSnake != Vector2.right)
            {
                Vector2 left = Vector2.left;
                directionSnake = left;
                canChangeDirection = false;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && directionSnake != Vector2.left)
            {
                Vector2 right = Vector2.right;
                directionSnake = right;
                canChangeDirection = false;
            }

        }

        // Reinicia la posici�n de la serpiente al presionar la tecla "R"
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetSnake();
        }
    }

    private void Grow()
    {
        /*
         * Crea una instancia de un nuevo GameObject a partir del prefab bodySnakePrefab
         * El Transform de bodySnake = referencia a la transformada del nuevo GameObject
         * Establece la posici�n bodySnake (nuevo body de la snake) = a la posici�n del �ltimo body de la serpiente en la lista bodysList (el nuevo body se coloque detr�s del �ltimo body existente).
         * Agrega el nuevo body de la snake a la lista bodysList
         * 
         */

        Transform bodySnake = Instantiate(this.bodySnakePrefab);
        bodySnake.position = bodysList[bodysList.Count - 1].position;
        bodysList.Add(bodySnake);
    }

    // Gestiona el inicio de la snake
    private void ResetSnake()
    {
        /*
         * Destruye los GameObject que est�n en la lista bodysSnake (bodys de la snake)
         * Limpia la lista bodysList
         * Adiciona a la lista bodysList la cabeza de la snake
         * Instancia en la lista bodysSnake la cantidad de bodys definido por initialSizeSnake, para que la snake tenga la cabeza y cuerpo
         * Posiciona la snake
         * Actualiza los puntos
         */
        for(int i = 1; i < bodysList.Count; i++) 
        {
            Destroy(bodysList[i].gameObject);
        }

        bodysList.Clear();
        bodysList.Add(this.transform);

        for(int i = 1; i < this.initialSizeSnake; i++)
        {
            bodysList.Add(Instantiate(this.bodySnakePrefab));
        }

        this.transform.position = Vector2.zero;

        gameManager.ResetScore();
    }

    // Gestiona el crecimiento de la snake
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Si la snake colisiona con la fruit => la snake crece llamando a la funci�n Grow() e incrementa los puntos en 1 y 
        // Sino => resetea la posici�n de la snake, y decrementa los puntos en 1.
        if (other.tag == "Fruit")
        {
            Grow(); 
            gameManager.AddScore(1);
            gameManager.Bodys(bodysList.Count - 1);

        }else if (other.tag == "Obstacle")
        {
            gameManager.ResetScore();
            ResetSnake();
            fruit.PositionFruit();
        }
    }
}
