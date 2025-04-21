using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadTransition : MonoBehaviour
{
    public GameObject imageToShrink;
    public float shrinkDuration = 1f;
    private Vector2 _originalScale;
    private LevelManager _levelManager;
    private HudManager _hudManager;

    private void Awake()
    {
        _levelManager = FindObjectOfType<LevelManager>();
        _hudManager = FindObjectOfType<HudManager>();
    }
    void Start()
    {
        if (imageToShrink != null)
        {
            _originalScale = new Vector2(100f, 100f);
            imageToShrink.transform.localScale = Vector2.zero;
        }
    }

    public void StartShrink()
    {
        if (imageToShrink != null)
        {
            imageToShrink.transform.localPosition = FindObjectOfType<PlayerMovement>().transform.position;
            StartCoroutine(ShrinkAndReset());
        }
    }

    private IEnumerator ShrinkAndReset()
    {
        float elapsedTime = 0f;
        while (elapsedTime < shrinkDuration)
        {
            imageToShrink.transform.localScale = Vector2.Lerp(_originalScale, Vector3.zero, elapsedTime / shrinkDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        imageToShrink.transform.localScale = Vector2.zero;
        FindObjectOfType<PlayerDie>().Die();

        _levelManager.InstantiateCharacter();
        imageToShrink.transform.localPosition = FindObjectOfType<PlayerMovement>().transform.position;
        _hudManager.UpdateHud();

        yield return new WaitForSeconds(0.5f); // Espera un segundo antes de restaurar
        elapsedTime = 0f;
        while (elapsedTime < shrinkDuration)
        {
            imageToShrink.transform.localScale = Vector2.Lerp(Vector2.zero, _originalScale, elapsedTime / shrinkDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        imageToShrink.transform.localScale = _originalScale;
    }

    public IEnumerator FadeOut()
    {
        imageToShrink.transform.localPosition = FindObjectOfType<PlayerMovement>().transform.position;
        imageToShrink.transform.localScale = Vector2.zero;
        float elapsedTime = 0f;
        while (elapsedTime < shrinkDuration)
        {
            imageToShrink.transform.localScale = Vector2.Lerp(Vector2.zero, _originalScale, elapsedTime / shrinkDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        imageToShrink.transform.localScale = _originalScale;
    }
}
