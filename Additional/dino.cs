
    public Dino dino;

    private void Start()
    {

        dino = dino ?? GetComponent<Dino>();
        dino.rgdB2D = GetComponent<Rigidbody2D>();
        if (dino == null)
        {
            Debug.LogError("Player not set to controller");
        }
    }

    private void Update()
    {

        if (dino != null)
        {

            if (Input.GetKey(KeyCode.RightArrow))
            {
                dino.MoveRight();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                dino.Jump();
            }
        }
    } 