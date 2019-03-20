using UnityEngine;


[ExecuteInEditMode]
public class PostProcess : MonoBehaviour
{
    public enum TimeOfTheDayState { Day, Night, TurningToDay, TurningToNight }
    public Material material;
    public float changeSpeed = .1f;
    public TimeOfTheDayState _state = TimeOfTheDayState.Day;

    public float _DayVRadius = 0.85f;
    public float _NightVRadius = 0.6f;

    private PostProcess postProcess;
    private float _VRadius = 0.85f;
    private float _VSoft = 0.7f;

    public void SetToDay()
    {
        _state = TimeOfTheDayState.TurningToDay;
    }

    public void SetToNight()
    {
        _state = TimeOfTheDayState.TurningToNight;
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, material);
    }

    private void Awake()
    {
        postProcess = GetComponent<PostProcess>();

        postProcess.material.SetFloat("_VRadius", 0.85f);
        postProcess.material.SetFloat("_VSoft", 0.7f);
    }

    private void Update()
    {
        switch(_state)
        {
            case TimeOfTheDayState.Day:
                break;
            case TimeOfTheDayState.Night:
                break;
            case TimeOfTheDayState.TurningToDay:
                if(_VRadius >= _DayVRadius)
                {
                    _state = TimeOfTheDayState.Day;
                }

                _VRadius += Time.deltaTime * changeSpeed;
                postProcess.material.SetFloat("_VRadius", _VRadius);
                break;
            case TimeOfTheDayState.TurningToNight:
                if (_VRadius <= _NightVRadius)
                {
                    _state = TimeOfTheDayState.Night;
                }

                _VRadius -= Time.deltaTime * changeSpeed;
                postProcess.material.SetFloat("_VRadius", _VRadius);
                break;
            default:
                break;
        }
    }
}