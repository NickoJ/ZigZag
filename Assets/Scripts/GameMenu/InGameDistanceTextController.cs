using System;
using Klyukay.KTools.DependencyInjection;
using Klyukay.ZigZag.Session;
using TMPro;
using UnityEngine;

namespace Klyukay.ZigZag.Unity.GameMenu
{
    
    [RequireComponent(typeof(TMP_Text))]
    public class InGameDistanceTextController : MonoBehaviour
    {

        [SerializeField] private string templateText = "Distance: {0:0000}";
        
        private SessionManager _sessionManager;
        
        private TMP_Text _text;
        
        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
        }

        private void Start()
        {
            var resolver = DI.GetResolver((int) GameScenes.GameScene);

            _sessionManager = resolver.Resolve<SessionManager>();
            _sessionManager.SessionStarted += SessionStarted;
            _sessionManager.SessionFinished += SessionFinished;

            enabled = _sessionManager.IsSessionStarted;
        }

        private void OnDestroy()
        {
            _sessionManager.SessionStarted -= SessionStarted;
            _sessionManager.SessionFinished -= SessionFinished;
        }

        private void LateUpdate()
        {
            _text.text = string.Format(templateText, (int)_sessionManager.TotalDistance);
        }

        private void SessionStarted() => enabled = true;
        private void SessionFinished() => enabled = false;

    }
    
}