using System.Collections;
using System.Collections.Generic;
using Core.Utilities.Singletons;
using UnityEngine;
using UnityEngine.Events;

namespace Core.Utilities.Coroutines
{
    public class CoroutineManager : SingletonBase<CoroutineManager>
    {
        public event UnityAction<bool> applicationPaused;
		public event UnityAction applicationQuitted;

		// Queued actions
		private static Queue<UnityAction> queuedActions = new Queue<UnityAction>();


		#region Unity life cycle

		private void Update()
		{
			ExecuteQueuedActions();
		}

		private void OnApplicationPause(bool paused)
		{
			applicationPaused?.Invoke(paused);
		}

		private void OnApplicationQuit()
		{
			applicationQuitted?.Invoke();
		}

		#endregion

		#region Invoke

		/// <summary>
		/// Executes a call action after a period of time.
		/// </summary>
		/// <param name="callAction">The call action to executed.</param>
		/// <param name="seconds">Seconds waiting to execute the action.</param>
		/// <returns></returns>
		public static IEnumerator Invoke(UnityAction callAction, float seconds, bool realtime = true)
		{
			IEnumerator routine = instance.InvokeCoroutine(callAction, seconds, realtime);
			instance.StartCoroutine(routine);
			return routine;
		}

		private IEnumerator InvokeCoroutine(UnityAction callAction, float seconds, bool realtime)
		{
			if (realtime)
				yield return new WaitForSecondsRealtime(seconds);
			else
				yield return new WaitForSeconds(seconds);
			callAction?.Invoke();
		}

		/// <summary>
		/// Executes a call action in repeating time. Notes that this will not stop and make sure cache the IEnumerator
		/// and stop accordingly.
		/// </summary>
		/// <param name="callAction">The call action to executed.</param>
		/// <param name="seconds">Seconds to start first iteration.</param>
		/// <param name="repeatRate">Seconds between each iteration.</param>
		/// <returns></returns>
		public static IEnumerator InvokeRepeating(UnityAction callAction, float seconds, float repeatRate)
		{
			IEnumerator routine = instance.InvokeRepeatingCoroutine(callAction, seconds, repeatRate);
			instance.StartCoroutine(routine);
			return routine;
		}

		private IEnumerator InvokeRepeatingCoroutine(UnityAction callAction, float seconds, float repeatRate)
		{
			yield return new WaitForSecondsRealtime(seconds);
			while (true) {
				callAction?.Invoke();
				yield return new WaitForSecondsRealtime(repeatRate);
			}
		}

		#endregion

		#region Coroutine

		public static IEnumerator Start(IEnumerator routine)
		{
			// This runs the coroutine correctly
			instance.StartCoroutine(routine);
			return routine;
		}

		public static void Stop(IEnumerator routine)
		{
			if (routine != null)
				instance.StopCoroutine(routine);
		}

		#endregion

		#region Wait for frames

		public static IEnumerator WaitForFrames(int frames, UnityAction onCompleteAction)
		{
			IEnumerator waitRoutine = WaitForFramesRoutine(frames, onCompleteAction);
			Start(waitRoutine);
			return waitRoutine;
		}

		private static IEnumerator WaitForFramesRoutine(int frames, UnityAction onCompleteAction)
		{
			for (int i = 0; i < frames; i++)
				yield return null;
			onCompleteAction?.Invoke();
		}

		#endregion

		#region Queue For Main Thread

		/// <summary>
		/// Queues a delegate action to execute on the next update frame.
		/// Can be used to delegate actions Unity Functions running on non Unity Threads
		/// </summary>
		/// <param name="delegateAction"></param>
		public static void QueueForNextUpdate(UnityAction delegateAction)
		{
			queuedActions.Enqueue(delegateAction);
		}

		private void ExecuteQueuedActions()
		{
			// Go through all of our Queued up actions
			if (queuedActions != null && queuedActions.Count > 0)
				queuedActions.Dequeue()?.Invoke();
		}

		#endregion
    }
}