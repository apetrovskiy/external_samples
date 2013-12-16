
using System;

namespace Remote
{
	[Serializable]
	public struct kAction
	{
		public string   s_Command;   // the text to be sent by master
		public string   s_Computer;  // Computer name of the sender (=slave)
		
		// to transfer more data expand here...
	};

	[Serializable]
	public struct kResponse
	{
		public string    s_Result;   // the response text sent by slave
		
		// to transfer more data expand here...
	};

	public class cTransfer : MarshalByRefObject
	{
		public delegate kResponse      del_SlaveCall(kAction k_Action);
		public event    del_SlaveCall  ev_SlaveCall;

		// Default public no argument constructor
		public cTransfer() 
		{
		}

		public kResponse CallSlave(kAction k_Action)
		{
			return ev_SlaveCall(k_Action);			
		}

		/// <summary>
		/// This override is EXTREMELY important
		/// If it is missing the garbage collector of the Slave will delete the cTransfer object
		/// after 5 minutes and the event will be lost, so further calls to the slave will return
		/// "Server encountered an internal error" (a very helpful Mircrosoft error message!)
		/// This function sets the livetime to infinite
		/// See http://www.thinktecture.com/Resources/RemotingFAQ/SINGLETON_IS_DYING.html
		/// </summary>
		public override Object InitializeLifetimeService()
		{
			return null;
		}
	}
}

