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
        _tutorialText.text = "�ȳ��ϼ���";
        yield return new WaitForSeconds(1.5f);
        _tutorialText.text = "���� ������� ���콺�� ���� �����Դϴ�.";
        yield return new WaitForSeconds(2f);
        _tutorialText.text = "����� �������� ���� ���콺�� ��ġ�� ���� �޶����µ�";
        yield return new WaitForSeconds(2.5f);
        _tutorialText.text = "�̴� ���� ó���� �� �ִ� �������� ��ġ�Դϴ�.";
        yield return new WaitForSeconds(2.5f);
        _tutorialText.text = SettingManager.Instance.currentKey.ToString() + "�� ������ ���� ���콺�� �ִ� ���� ��Ʈ�� ó���˴ϴ�.";
        yield return new WaitForSeconds(2.5f);
        _tutorialText.text = SettingManager.Instance.oppositeKey.ToString() + "�� ������ ���� ���콺�� �ִ� ���� �ݴ��� ��Ʈ�� ó���˴ϴ�.";
        yield return new WaitForSeconds(2.5f);
        _tutorialText.text = "�ѹ� �غ����?";
        yield return new WaitForSeconds(2f);
        _tutorialText.text = "";
        yield return new WaitForSeconds(18f);
        _tutorialText.text = "���� �ճ�Ʈ �Դϴ�! �� ��������!";
        yield return new WaitForSeconds(2.5f);
        _tutorialText.text = "";
        yield return new WaitForSeconds(9f);
        _tutorialText.text = "�Ķ��� ��Ʈ�� Ű�� �� ������ �־ ó���� �Ǵ� ��Ʈ�Դϴ�.";
        yield return new WaitForSeconds(2.5f);
        _tutorialText.text = "�� ���� �� �巡�� �Ѵٴ� �������� ó���غ�����.";
        yield return new WaitForSeconds(4f);
        _tutorialText.text = "";
        yield return new WaitForSeconds(12f);
        _tutorialText.text = "�����ϴ�!";
        AudioManager.Instance.audioSource.DOFade(0, 1f);
        GameManager.Instance.GameEnd();
    }
}
