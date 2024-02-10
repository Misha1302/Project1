namespace NamehaveCat.Scripts.Machinery
{
    using System.Collections;
    using NamehaveCat.Scripts.Different;
    using NamehaveCat.Scripts.MImplementations;
    using UnityEngine;

    public class PhysicsButton : ElectricitySource
    {
        private readonly RaycastHit2D[] _results = new RaycastHit2D[128];

        private void Start()
        {
            GameManager.Instance.CoroutineManager.StartCoroutine(SlowUpdate());
        }

        private IEnumerator SlowUpdate()
        {
            while (true)
            {
                // ReSharper disable once Unity.InefficientPropertyAccess
                var len = Physics2D.BoxCastNonAlloc(
                    transform.position, transform.lossyScale, 0, Vector2.zero, _results
                );

                HasElectricity = false;
                for (var i = 0; i < len; i++)
                    // ReSharper disable once AssignmentInConditionalExpression
                    if (_results[i].transform.TryGetComponent<Rigidbody2D>(out _))
                    {
                        HasElectricity = true;
                        break;
                    }

                yield return new MWaitForSeconds(0.1f);
            }

            // ReSharper disable once IteratorNeverReturns
        }
    }
}