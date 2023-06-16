#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "Version Manager", menuName = "Scriptable Objects/Tooling/VersionManager")]
public class S_ApplicationVersionManager : ScriptableObject
{
    [SerializeField, ReadOnly]
    private string _currentVersion;

    [SerializeField, ReadOnly]
    private string _newVersion;

    [SerializeField]
    private bool _linkIncrements = true;

    [SerializeField]
    private bool _editVersionDirectly = false;

    [SerializeField, ShowIf("_editVersionDirectly"), InfoBox("Be careful with this", EInfoBoxType.Warning)]
    private int[] _internalVersion = new int[4];

    public void OnEnable()
    {
        Reset();
    }

    [Button]
    public void IncrementVersion()
    {
        ArrayIncrement(0);
    }

    [Button]
    public void IncrementUpdate()
    {
        ArrayIncrement(1);
    }

    [Button]
    public void IncrementPatch()
    {
        ArrayIncrement(2);
    }

    [Button]
    public void IncrementHotfix()
    {
        ArrayIncrement(3);
    }

    [Button]
    public void ApplyVersion()
    {
        UpdateNewVersion();
        PlayerSettings.bundleVersion = _newVersion;
        _currentVersion = Application.version;
    }

    [Button]
    public void Reset()
    {
        string[] ver = new string[4];
        ver = Application.version.Split('.');

        for (int i = 0; i < ver.Length; i++)
        {
            if (int.TryParse(ver[i], out int result))
            {
                _internalVersion[i] = result;
            }
            else
            {
                _internalVersion[i] = 0;
            }
        }
        _currentVersion = Application.version;
        _newVersion = _currentVersion;
    }

    private void ArrayIncrement(int pIndex)
    {

        if (_linkIncrements)
        {
            for (int i = pIndex + 1; i < _internalVersion.Length; i++)
            {
                _internalVersion[i] = 0;
            }
        }

        _internalVersion[pIndex]++;

        UpdateNewVersion();
    }


    private void UpdateNewVersion()
    {
        _newVersion = "";

        for (int i = 0; i < _internalVersion.Length - 1; i++)
        {
            _newVersion += _internalVersion[i].ToString() + '.';
        }

        _newVersion += _internalVersion[_internalVersion.Length - 1].ToString();
    }
}

#endif

