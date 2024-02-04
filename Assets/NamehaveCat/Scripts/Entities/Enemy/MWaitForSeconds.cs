namespace NamehaveCat.Scripts.Entities.Enemy
{
    using NamehaveCat.Scripts.Different;
    using UnityEngine;

    public class MWaitForSeconds : CustomYieldInstruction
    {
        private readonly float _stopTime;

        public MWaitForSeconds(float stopTime)
        {
            _stopTime = GameManager.Instance.Time.CurTime + stopTime;
        }

        public override bool keepWaiting => GameManager.Instance.Time.CurTime < _stopTime;
    }
}