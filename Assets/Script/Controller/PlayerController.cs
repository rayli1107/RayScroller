using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    private int _speed;
    [SerializeField]
    private int _jumpSpeed = 200;
    [SerializeField]
    private Color _maskNormal;
    [SerializeField]
    private Color _maskInjured;
    [SerializeField]
    private float _jumpDuration = 0.3f;
    [SerializeField]
    private float _injuredInvincibleDuration = 0.5f;
#pragma warning restore 0649

    private Rigidbody2D _body;
    private int _dx;
    private float _timeJumpStart = -1f;
    private float _timeDamageStart = -1f;
    private bool _canJump;
    private bool _damage;
    private SpriteRenderer _sprite;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
        _sprite.color = _maskNormal;
        _dx = 0;
        _timeJumpStart = -1f;
        _timeDamageStart = -1f;
        _canJump = false;
        _damage = false;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        PlatformController platform = collision.gameObject.GetComponent<PlatformController>();
        if (platform != null)
        {
            _canJump = true;
            _damage = platform.damage;
        }

        CoinController coin = collision.gameObject.GetComponent<CoinController>();
        if (coin != null)
        {
            AudioManager.Instance.PlayCoin();
            ++GlobalGameContext.coin;
            Destroy(coin.gameObject);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        PlatformController platform = collision.gameObject.GetComponent<PlatformController>();
        if (platform != null)
        {
            _canJump = false;
            _damage = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        _dx = 0;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            ++_dx;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            --_dx;
        }

        if (_dx > 0)
        {
            transform.localScale = new Vector3(
                Mathf.Abs(transform.localScale.x),
                transform.localScale.y,
                transform.localScale.z);
        }
        else if (_dx < 0)
        {
            transform.localScale = new Vector3(
                -1 * Mathf.Abs(transform.localScale.x),
                transform.localScale.y,
                transform.localScale.z);
        }

        float timeNow = Time.time;
        if (_canJump && Input.GetKeyDown(KeyCode.Space))
        {
            _timeJumpStart = timeNow;
            AudioManager.Instance.PlayJump();
        }
        else if (_timeJumpStart >= 0f && timeNow - _timeJumpStart >= _jumpDuration)
        {
            _timeJumpStart = -1f;
        }
        if (_timeJumpStart >= 0f)
        {
            _body.AddRelativeForce(new Vector2(0f, 300));
        }

        if (_damage && _timeDamageStart < 0f)
        {
            Damage();
            _timeDamageStart = timeNow;
            _sprite.color = _maskInjured;
        }
        if (_timeDamageStart >= 0f && timeNow - _timeDamageStart >= _injuredInvincibleDuration)
        {
            _timeDamageStart = -1f;
            _sprite.color = _maskNormal;
        }
    }

    private void FixedUpdate()
    {
        _body.velocity = new Vector3(
            _dx * _speed * Time.fixedDeltaTime,
            0f,
//            _timeJumpStart >= 0f ? _jumpSpeed * Time.fixedDeltaTime : 0f,
            0f);
    }

    public void Kill()
    {
        GlobalGameContext.hp = 0;
    }

    public void Damage()
    {
        --GlobalGameContext.hp;
        if (GlobalGameContext.hp > 0)
        {
            AudioManager.Instance.PlayDamage();
        }
    }
}
