using System.Collections;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class Tutorial : MonoBehaviour
{
    TextMeshProUGUI _tutorialText;

    private void Awake()
    {
        _tutorialText = transform.Find("TutorialText").GetComponent<TextMeshProUGUI>();
    }
    private void Start()
    {
        if (LevelManager.Instance.levelSO.name == "Tutorial")
        {
            StartCoroutine(IETutorial());
        }
    }

    IEnumerator IETutorial()
    {
        yield return new WaitUntil(() => AudioManager.Instance.audioSource.isPlaying);
        _tutorialText.text = "안녕하세요";
        yield return new WaitForSeconds(1.5f);
        _tutorialText.text = "저기 흰색빛은 마우스를 따라 움직입니다.";
        yield return new WaitForSeconds(2f);
        _tutorialText.text = "가운데의 육각형을 보면 마우스의 위치에 따라 달라지는데";
        yield return new WaitForSeconds(2.5f);
        _tutorialText.text = "이는 지금 처리할 수 있는 판정선의 위치입니다.";
        yield return new WaitForSeconds(2.5f);
        _tutorialText.text = SettingManager.Instance.currentKey.ToString() + "를 누르면 지금 마우스가 있는 곳의 노트가 처리됩니다.";
        yield return new WaitForSeconds(2.5f);
        _tutorialText.text = SettingManager.Instance.oppositeKey.ToString() + "를 누르면 지금 마우스가 있는 곳의 반대쪽 노트가 처리됩니다.";
        yield return new WaitForSeconds(2.5f);
        _tutorialText.text = "한번 해볼까요?";
        yield return new WaitForSeconds(2f);
        _tutorialText.text = "";
        yield return new WaitForSeconds(18f);
        _tutorialText.text = "저건 롱노트 입니다! 꾹 누르세요!";
        yield return new WaitForSeconds(2.5f);
        _tutorialText.text = "";
        yield return new WaitForSeconds(9f);
        _tutorialText.text = "파란색 노트는 키를 꾹 누르고 있어도 처리가 되는 노트입니다.";
        yield return new WaitForSeconds(2.5f);
        _tutorialText.text = "꾹 누른 후 드래그 한다는 느낌으로 처리해보세요.";
        yield return new WaitForSeconds(4f);
        _tutorialText.text = "";
        yield return new WaitForSeconds(12f);
        _tutorialText.text = "좋습니다!";
        AudioManager.Instance.audioSource.DOFade(0, 1f);
        GameManager.Instance.GameEnd();
    }
}
