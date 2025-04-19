using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.VisualBasic.CompilerServices;

namespace Vibration_Evaluation.My
{
	// Token: 0x02000004 RID: 4
	[StandardModule]
	[HideModuleName]
	[GeneratedCode("MyTemplate", "11.0.0.0")]
	internal sealed class MyProject
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000006 RID: 6 RVA: 0x0000210C File Offset: 0x0000030C
		[HelpKeyword("My.Computer")]
		internal static MyComputer Computer
		{
			[DebuggerHidden]
			get
			{
				return MyProject.m_ComputerObjectProvider.GetInstance;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002128 File Offset: 0x00000328
		[HelpKeyword("My.Application")]
		internal static MyApplication Application
		{
			[DebuggerHidden]
			get
			{
				return MyProject.m_AppObjectProvider.GetInstance;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002144 File Offset: 0x00000344
		[HelpKeyword("My.User")]
		internal static User User
		{
			[DebuggerHidden]
			get
			{
				return MyProject.m_UserObjectProvider.GetInstance;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00002160 File Offset: 0x00000360
		[HelpKeyword("My.Forms")]
		internal static MyProject.MyForms Forms
		{
			[DebuggerHidden]
			get
			{
				return MyProject.m_MyFormsObjectProvider.GetInstance;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000A RID: 10 RVA: 0x0000217C File Offset: 0x0000037C
		[HelpKeyword("My.WebServices")]
		internal static MyProject.MyWebServices WebServices
		{
			[DebuggerHidden]
			get
			{
				return MyProject.m_MyWebServicesObjectProvider.GetInstance;
			}
		}

		// Token: 0x04000001 RID: 1
		private static readonly MyProject.ThreadSafeObjectProvider<MyComputer> m_ComputerObjectProvider = new MyProject.ThreadSafeObjectProvider<MyComputer>();

		// Token: 0x04000002 RID: 2
		private static readonly MyProject.ThreadSafeObjectProvider<MyApplication> m_AppObjectProvider = new MyProject.ThreadSafeObjectProvider<MyApplication>();

		// Token: 0x04000003 RID: 3
		private static readonly MyProject.ThreadSafeObjectProvider<User> m_UserObjectProvider = new MyProject.ThreadSafeObjectProvider<User>();

		// Token: 0x04000004 RID: 4
		private static MyProject.ThreadSafeObjectProvider<MyProject.MyForms> m_MyFormsObjectProvider = new MyProject.ThreadSafeObjectProvider<MyProject.MyForms>();

		// Token: 0x04000005 RID: 5
		private static readonly MyProject.ThreadSafeObjectProvider<MyProject.MyWebServices> m_MyWebServicesObjectProvider = new MyProject.ThreadSafeObjectProvider<MyProject.MyWebServices>();

		// Token: 0x02000024 RID: 36
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MyGroupCollection("System.Windows.Forms.Form", "Create__Instance__", "Dispose__Instance__", "My.MyProject.Forms")]
		internal sealed class MyForms
		{
			// Token: 0x060003D7 RID: 983 RVA: 0x0001B288 File Offset: 0x00019488
			[DebuggerHidden]
			private static T Create__Instance__<T>(T Instance) where T : Form, new()
			{
				bool flag = Instance == null || Instance.IsDisposed;
				if (flag)
				{
					bool flag2 = MyProject.MyForms.m_FormBeingCreated != null;
					if (flag2)
					{
						bool flag3 = MyProject.MyForms.m_FormBeingCreated.ContainsKey(typeof(T));
						if (flag3)
						{
							throw new InvalidOperationException(Utils.GetResourceString("WinForms_RecursiveFormCreate", new string[0]));
						}
					}
					else
					{
						MyProject.MyForms.m_FormBeingCreated = new Hashtable();
					}
					MyProject.MyForms.m_FormBeingCreated.Add(typeof(T), null);
					try
					{
						return Activator.CreateInstance<T>();
					}
					catch (TargetInvocationException ex) when (ex.InnerException != null)
					{
						string resourceString = Utils.GetResourceString("WinForms_SeeInnerException", new string[]
						{
							ex.InnerException.Message
						});
						throw new InvalidOperationException(resourceString, ex.InnerException);
					}
					finally
					{
						MyProject.MyForms.m_FormBeingCreated.Remove(typeof(T));
					}
				}
				return Instance;
			}

			// Token: 0x060003D8 RID: 984 RVA: 0x0001B3B0 File Offset: 0x000195B0
			[DebuggerHidden]
			private void Dispose__Instance__<T>(ref T instance) where T : Form
			{
				instance.Dispose();
				instance = default(T);
			}

			// Token: 0x060003D9 RID: 985 RVA: 0x0001B3C7 File Offset: 0x000195C7
			[DebuggerHidden]
			[EditorBrowsable(EditorBrowsableState.Never)]
			public MyForms()
			{
			}

			// Token: 0x060003DA RID: 986 RVA: 0x0001B3D4 File Offset: 0x000195D4
			[EditorBrowsable(EditorBrowsableState.Never)]
			public override bool Equals(object o)
			{
				return base.Equals(RuntimeHelpers.GetObjectValue(o));
			}

			// Token: 0x060003DB RID: 987 RVA: 0x0001B3F4 File Offset: 0x000195F4
			[EditorBrowsable(EditorBrowsableState.Never)]
			public override int GetHashCode()
			{
				return base.GetHashCode();
			}

			// Token: 0x060003DC RID: 988 RVA: 0x0001B40C File Offset: 0x0001960C
			[EditorBrowsable(EditorBrowsableState.Never)]
			internal new Type GetType()
			{
				return typeof(MyProject.MyForms);
			}

			// Token: 0x060003DD RID: 989 RVA: 0x0001B428 File Offset: 0x00019628
			[EditorBrowsable(EditorBrowsableState.Never)]
			public override string ToString()
			{
				return base.ToString();
			}

			// Token: 0x17000143 RID: 323
			// (get) Token: 0x060003DE RID: 990 RVA: 0x0001B440 File Offset: 0x00019640
			// (set) Token: 0x060003EE RID: 1006 RVA: 0x0001B5F0 File Offset: 0x000197F0
			public AboutBox1 AboutBox1
			{
				[DebuggerHidden]
				get
				{
					this.m_AboutBox1 = MyProject.MyForms.Create__Instance__<AboutBox1>(this.m_AboutBox1);
					return this.m_AboutBox1;
				}
				[DebuggerHidden]
				set
				{
					if (value != this.m_AboutBox1)
					{
						if (value != null)
						{
							throw new ArgumentException("Property can only be set to Nothing");
						}
						this.Dispose__Instance__<AboutBox1>(ref this.m_AboutBox1);
					}
				}
			}

			// Token: 0x17000144 RID: 324
			// (get) Token: 0x060003DF RID: 991 RVA: 0x0001B45B File Offset: 0x0001965B
			// (set) Token: 0x060003EF RID: 1007 RVA: 0x0001B61C File Offset: 0x0001981C
			public DialogCheckListBox DialogCheckListBox
			{
				[DebuggerHidden]
				get
				{
					this.m_DialogCheckListBox = MyProject.MyForms.Create__Instance__<DialogCheckListBox>(this.m_DialogCheckListBox);
					return this.m_DialogCheckListBox;
				}
				[DebuggerHidden]
				set
				{
					if (value != this.m_DialogCheckListBox)
					{
						if (value != null)
						{
							throw new ArgumentException("Property can only be set to Nothing");
						}
						this.Dispose__Instance__<DialogCheckListBox>(ref this.m_DialogCheckListBox);
					}
				}
			}

			// Token: 0x17000145 RID: 325
			// (get) Token: 0x060003E0 RID: 992 RVA: 0x0001B476 File Offset: 0x00019676
			// (set) Token: 0x060003F0 RID: 1008 RVA: 0x0001B648 File Offset: 0x00019848
			public DialogUserInteger DialogUserInteger
			{
				[DebuggerHidden]
				get
				{
					this.m_DialogUserInteger = MyProject.MyForms.Create__Instance__<DialogUserInteger>(this.m_DialogUserInteger);
					return this.m_DialogUserInteger;
				}
				[DebuggerHidden]
				set
				{
					if (value != this.m_DialogUserInteger)
					{
						if (value != null)
						{
							throw new ArgumentException("Property can only be set to Nothing");
						}
						this.Dispose__Instance__<DialogUserInteger>(ref this.m_DialogUserInteger);
					}
				}
			}

			// Token: 0x17000146 RID: 326
			// (get) Token: 0x060003E1 RID: 993 RVA: 0x0001B491 File Offset: 0x00019691
			// (set) Token: 0x060003F1 RID: 1009 RVA: 0x0001B674 File Offset: 0x00019874
			public FormAlarmStatus2 FormAlarmStatus2
			{
				[DebuggerHidden]
				get
				{
					this.m_FormAlarmStatus2 = MyProject.MyForms.Create__Instance__<FormAlarmStatus2>(this.m_FormAlarmStatus2);
					return this.m_FormAlarmStatus2;
				}
				[DebuggerHidden]
				set
				{
					if (value != this.m_FormAlarmStatus2)
					{
						if (value != null)
						{
							throw new ArgumentException("Property can only be set to Nothing");
						}
						this.Dispose__Instance__<FormAlarmStatus2>(ref this.m_FormAlarmStatus2);
					}
				}
			}

			// Token: 0x17000147 RID: 327
			// (get) Token: 0x060003E2 RID: 994 RVA: 0x0001B4AC File Offset: 0x000196AC
			// (set) Token: 0x060003F2 RID: 1010 RVA: 0x0001B6A0 File Offset: 0x000198A0
			public FormAlarmValues FormAlarmValues
			{
				[DebuggerHidden]
				get
				{
					this.m_FormAlarmValues = MyProject.MyForms.Create__Instance__<FormAlarmValues>(this.m_FormAlarmValues);
					return this.m_FormAlarmValues;
				}
				[DebuggerHidden]
				set
				{
					if (value != this.m_FormAlarmValues)
					{
						if (value != null)
						{
							throw new ArgumentException("Property can only be set to Nothing");
						}
						this.Dispose__Instance__<FormAlarmValues>(ref this.m_FormAlarmValues);
					}
				}
			}

			// Token: 0x17000148 RID: 328
			// (get) Token: 0x060003E3 RID: 995 RVA: 0x0001B4C7 File Offset: 0x000196C7
			// (set) Token: 0x060003F3 RID: 1011 RVA: 0x0001B6CC File Offset: 0x000198CC
			public FormCountDown FormCountDown
			{
				[DebuggerHidden]
				get
				{
					this.m_FormCountDown = MyProject.MyForms.Create__Instance__<FormCountDown>(this.m_FormCountDown);
					return this.m_FormCountDown;
				}
				[DebuggerHidden]
				set
				{
					if (value != this.m_FormCountDown)
					{
						if (value != null)
						{
							throw new ArgumentException("Property can only be set to Nothing");
						}
						this.Dispose__Instance__<FormCountDown>(ref this.m_FormCountDown);
					}
				}
			}

			// Token: 0x17000149 RID: 329
			// (get) Token: 0x060003E4 RID: 996 RVA: 0x0001B4E2 File Offset: 0x000196E2
			// (set) Token: 0x060003F4 RID: 1012 RVA: 0x0001B6F8 File Offset: 0x000198F8
			public FormDataLog FormDataLog
			{
				[DebuggerHidden]
				get
				{
					this.m_FormDataLog = MyProject.MyForms.Create__Instance__<FormDataLog>(this.m_FormDataLog);
					return this.m_FormDataLog;
				}
				[DebuggerHidden]
				set
				{
					if (value != this.m_FormDataLog)
					{
						if (value != null)
						{
							throw new ArgumentException("Property can only be set to Nothing");
						}
						this.Dispose__Instance__<FormDataLog>(ref this.m_FormDataLog);
					}
				}
			}

			// Token: 0x1700014A RID: 330
			// (get) Token: 0x060003E5 RID: 997 RVA: 0x0001B4FD File Offset: 0x000196FD
			// (set) Token: 0x060003F5 RID: 1013 RVA: 0x0001B724 File Offset: 0x00019924
			public FormHelp FormHelp
			{
				[DebuggerHidden]
				get
				{
					this.m_FormHelp = MyProject.MyForms.Create__Instance__<FormHelp>(this.m_FormHelp);
					return this.m_FormHelp;
				}
				[DebuggerHidden]
				set
				{
					if (value != this.m_FormHelp)
					{
						if (value != null)
						{
							throw new ArgumentException("Property can only be set to Nothing");
						}
						this.Dispose__Instance__<FormHelp>(ref this.m_FormHelp);
					}
				}
			}

			// Token: 0x1700014B RID: 331
			// (get) Token: 0x060003E6 RID: 998 RVA: 0x0001B518 File Offset: 0x00019718
			// (set) Token: 0x060003F6 RID: 1014 RVA: 0x0001B750 File Offset: 0x00019950
			public FormMain FormMain
			{
				[DebuggerHidden]
				get
				{
					this.m_FormMain = MyProject.MyForms.Create__Instance__<FormMain>(this.m_FormMain);
					return this.m_FormMain;
				}
				[DebuggerHidden]
				set
				{
					if (value != this.m_FormMain)
					{
						if (value != null)
						{
							throw new ArgumentException("Property can only be set to Nothing");
						}
						this.Dispose__Instance__<FormMain>(ref this.m_FormMain);
					}
				}
			}

			// Token: 0x1700014C RID: 332
			// (get) Token: 0x060003E7 RID: 999 RVA: 0x0001B533 File Offset: 0x00019733
			// (set) Token: 0x060003F7 RID: 1015 RVA: 0x0001B77C File Offset: 0x0001997C
			public FormMessage FormMessage
			{
				[DebuggerHidden]
				get
				{
					this.m_FormMessage = MyProject.MyForms.Create__Instance__<FormMessage>(this.m_FormMessage);
					return this.m_FormMessage;
				}
				[DebuggerHidden]
				set
				{
					if (value != this.m_FormMessage)
					{
						if (value != null)
						{
							throw new ArgumentException("Property can only be set to Nothing");
						}
						this.Dispose__Instance__<FormMessage>(ref this.m_FormMessage);
					}
				}
			}

			// Token: 0x1700014D RID: 333
			// (get) Token: 0x060003E8 RID: 1000 RVA: 0x0001B54E File Offset: 0x0001974E
			// (set) Token: 0x060003F8 RID: 1016 RVA: 0x0001B7A8 File Offset: 0x000199A8
			public FormRegAccess FormRegAccess
			{
				[DebuggerHidden]
				get
				{
					this.m_FormRegAccess = MyProject.MyForms.Create__Instance__<FormRegAccess>(this.m_FormRegAccess);
					return this.m_FormRegAccess;
				}
				[DebuggerHidden]
				set
				{
					if (value != this.m_FormRegAccess)
					{
						if (value != null)
						{
							throw new ArgumentException("Property can only be set to Nothing");
						}
						this.Dispose__Instance__<FormRegAccess>(ref this.m_FormRegAccess);
					}
				}
			}

			// Token: 0x1700014E RID: 334
			// (get) Token: 0x060003E9 RID: 1001 RVA: 0x0001B569 File Offset: 0x00019769
			// (set) Token: 0x060003F9 RID: 1017 RVA: 0x0001B7D4 File Offset: 0x000199D4
			public FormRichText FormRichText
			{
				[DebuggerHidden]
				get
				{
					this.m_FormRichText = MyProject.MyForms.Create__Instance__<FormRichText>(this.m_FormRichText);
					return this.m_FormRichText;
				}
				[DebuggerHidden]
				set
				{
					if (value != this.m_FormRichText)
					{
						if (value != null)
						{
							throw new ArgumentException("Property can only be set to Nothing");
						}
						this.Dispose__Instance__<FormRichText>(ref this.m_FormRichText);
					}
				}
			}

			// Token: 0x1700014F RID: 335
			// (get) Token: 0x060003EA RID: 1002 RVA: 0x0001B584 File Offset: 0x00019784
			// (set) Token: 0x060003FA RID: 1018 RVA: 0x0001B800 File Offset: 0x00019A00
			public FormSPI FormSPI
			{
				[DebuggerHidden]
				get
				{
					this.m_FormSPI = MyProject.MyForms.Create__Instance__<FormSPI>(this.m_FormSPI);
					return this.m_FormSPI;
				}
				[DebuggerHidden]
				set
				{
					if (value != this.m_FormSPI)
					{
						if (value != null)
						{
							throw new ArgumentException("Property can only be set to Nothing");
						}
						this.Dispose__Instance__<FormSPI>(ref this.m_FormSPI);
					}
				}
			}

			// Token: 0x17000150 RID: 336
			// (get) Token: 0x060003EB RID: 1003 RVA: 0x0001B59F File Offset: 0x0001979F
			// (set) Token: 0x060003FB RID: 1019 RVA: 0x0001B82C File Offset: 0x00019A2C
			public FormUSBConnect FormUSBConnect
			{
				[DebuggerHidden]
				get
				{
					this.m_FormUSBConnect = MyProject.MyForms.Create__Instance__<FormUSBConnect>(this.m_FormUSBConnect);
					return this.m_FormUSBConnect;
				}
				[DebuggerHidden]
				set
				{
					if (value != this.m_FormUSBConnect)
					{
						if (value != null)
						{
							throw new ArgumentException("Property can only be set to Nothing");
						}
						this.Dispose__Instance__<FormUSBConnect>(ref this.m_FormUSBConnect);
					}
				}
			}

			// Token: 0x17000151 RID: 337
			// (get) Token: 0x060003EC RID: 1004 RVA: 0x0001B5BA File Offset: 0x000197BA
			// (set) Token: 0x060003FC RID: 1020 RVA: 0x0001B858 File Offset: 0x00019A58
			public FormUserSec FormUserSec
			{
				[DebuggerHidden]
				get
				{
					this.m_FormUserSec = MyProject.MyForms.Create__Instance__<FormUserSec>(this.m_FormUserSec);
					return this.m_FormUserSec;
				}
				[DebuggerHidden]
				set
				{
					if (value != this.m_FormUserSec)
					{
						if (value != null)
						{
							throw new ArgumentException("Property can only be set to Nothing");
						}
						this.Dispose__Instance__<FormUserSec>(ref this.m_FormUserSec);
					}
				}
			}

			// Token: 0x17000152 RID: 338
			// (get) Token: 0x060003ED RID: 1005 RVA: 0x0001B5D5 File Offset: 0x000197D5
			// (set) Token: 0x060003FD RID: 1021 RVA: 0x0001B884 File Offset: 0x00019A84
			public FormXX FormXX
			{
				[DebuggerHidden]
				get
				{
					this.m_FormXX = MyProject.MyForms.Create__Instance__<FormXX>(this.m_FormXX);
					return this.m_FormXX;
				}
				[DebuggerHidden]
				set
				{
					if (value != this.m_FormXX)
					{
						if (value != null)
						{
							throw new ArgumentException("Property can only be set to Nothing");
						}
						this.Dispose__Instance__<FormXX>(ref this.m_FormXX);
					}
				}
			}

			// Token: 0x040001A7 RID: 423
			[ThreadStatic]
			private static Hashtable m_FormBeingCreated;

			// Token: 0x040001A8 RID: 424
			[EditorBrowsable(EditorBrowsableState.Never)]
			public AboutBox1 m_AboutBox1;

			// Token: 0x040001A9 RID: 425
			[EditorBrowsable(EditorBrowsableState.Never)]
			public DialogCheckListBox m_DialogCheckListBox;

			// Token: 0x040001AA RID: 426
			[EditorBrowsable(EditorBrowsableState.Never)]
			public DialogUserInteger m_DialogUserInteger;

			// Token: 0x040001AB RID: 427
			[EditorBrowsable(EditorBrowsableState.Never)]
			public FormAlarmStatus2 m_FormAlarmStatus2;

			// Token: 0x040001AC RID: 428
			[EditorBrowsable(EditorBrowsableState.Never)]
			public FormAlarmValues m_FormAlarmValues;

			// Token: 0x040001AD RID: 429
			[EditorBrowsable(EditorBrowsableState.Never)]
			public FormCountDown m_FormCountDown;

			// Token: 0x040001AE RID: 430
			[EditorBrowsable(EditorBrowsableState.Never)]
			public FormDataLog m_FormDataLog;

			// Token: 0x040001AF RID: 431
			[EditorBrowsable(EditorBrowsableState.Never)]
			public FormHelp m_FormHelp;

			// Token: 0x040001B0 RID: 432
			[EditorBrowsable(EditorBrowsableState.Never)]
			public FormMain m_FormMain;

			// Token: 0x040001B1 RID: 433
			[EditorBrowsable(EditorBrowsableState.Never)]
			public FormMessage m_FormMessage;

			// Token: 0x040001B2 RID: 434
			[EditorBrowsable(EditorBrowsableState.Never)]
			public FormRegAccess m_FormRegAccess;

			// Token: 0x040001B3 RID: 435
			[EditorBrowsable(EditorBrowsableState.Never)]
			public FormRichText m_FormRichText;

			// Token: 0x040001B4 RID: 436
			[EditorBrowsable(EditorBrowsableState.Never)]
			public FormSPI m_FormSPI;

			// Token: 0x040001B5 RID: 437
			[EditorBrowsable(EditorBrowsableState.Never)]
			public FormUSBConnect m_FormUSBConnect;

			// Token: 0x040001B6 RID: 438
			[EditorBrowsable(EditorBrowsableState.Never)]
			public FormUserSec m_FormUserSec;

			// Token: 0x040001B7 RID: 439
			[EditorBrowsable(EditorBrowsableState.Never)]
			public FormXX m_FormXX;
		}

		// Token: 0x02000025 RID: 37
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MyGroupCollection("System.Web.Services.Protocols.SoapHttpClientProtocol", "Create__Instance__", "Dispose__Instance__", "")]
		internal sealed class MyWebServices
		{
			// Token: 0x060003FE RID: 1022 RVA: 0x0001B8B0 File Offset: 0x00019AB0
			[EditorBrowsable(EditorBrowsableState.Never)]
			[DebuggerHidden]
			public override bool Equals(object o)
			{
				return base.Equals(RuntimeHelpers.GetObjectValue(o));
			}

			// Token: 0x060003FF RID: 1023 RVA: 0x0001B8D0 File Offset: 0x00019AD0
			[EditorBrowsable(EditorBrowsableState.Never)]
			[DebuggerHidden]
			public override int GetHashCode()
			{
				return base.GetHashCode();
			}

			// Token: 0x06000400 RID: 1024 RVA: 0x0001B8E8 File Offset: 0x00019AE8
			[EditorBrowsable(EditorBrowsableState.Never)]
			[DebuggerHidden]
			internal new Type GetType()
			{
				return typeof(MyProject.MyWebServices);
			}

			// Token: 0x06000401 RID: 1025 RVA: 0x0001B904 File Offset: 0x00019B04
			[EditorBrowsable(EditorBrowsableState.Never)]
			[DebuggerHidden]
			public override string ToString()
			{
				return base.ToString();
			}

			// Token: 0x06000402 RID: 1026 RVA: 0x0001B91C File Offset: 0x00019B1C
			[DebuggerHidden]
			private static T Create__Instance__<T>(T instance) where T : new()
			{
				bool flag = instance == null;
				T result;
				if (flag)
				{
					result = Activator.CreateInstance<T>();
				}
				else
				{
					result = instance;
				}
				return result;
			}

			// Token: 0x06000403 RID: 1027 RVA: 0x0001B945 File Offset: 0x00019B45
			[DebuggerHidden]
			private void Dispose__Instance__<T>(ref T instance)
			{
				instance = default(T);
			}

			// Token: 0x06000404 RID: 1028 RVA: 0x0001B3C7 File Offset: 0x000195C7
			[DebuggerHidden]
			[EditorBrowsable(EditorBrowsableState.Never)]
			public MyWebServices()
			{
			}
		}

		// Token: 0x02000026 RID: 38
		[EditorBrowsable(EditorBrowsableState.Never)]
		[ComVisible(false)]
		internal sealed class ThreadSafeObjectProvider<T> where T : new()
		{
			// Token: 0x17000153 RID: 339
			// (get) Token: 0x06000405 RID: 1029 RVA: 0x0001B950 File Offset: 0x00019B50
			internal T GetInstance
			{
				[DebuggerHidden]
				get
				{
					bool flag = MyProject.ThreadSafeObjectProvider<T>.m_ThreadStaticValue == null;
					if (flag)
					{
						MyProject.ThreadSafeObjectProvider<T>.m_ThreadStaticValue = Activator.CreateInstance<T>();
					}
					return MyProject.ThreadSafeObjectProvider<T>.m_ThreadStaticValue;
				}
			}

			// Token: 0x06000406 RID: 1030 RVA: 0x0001B3C7 File Offset: 0x000195C7
			[DebuggerHidden]
			[EditorBrowsable(EditorBrowsableState.Never)]
			public ThreadSafeObjectProvider()
			{
			}

			// Token: 0x040001B8 RID: 440
			[CompilerGenerated]
			[ThreadStatic]
			private static T m_ThreadStaticValue;
		}
	}
}
