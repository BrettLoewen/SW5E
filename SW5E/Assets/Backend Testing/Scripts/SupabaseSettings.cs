using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Brett.Experiments.Supabase
{
	[CreateAssetMenu(fileName = "Supabase", menuName = "Experiments/Supabase/Supabase Settings", order = 1)]
	public class SupabaseSettings : ScriptableObject
	{
		public string SupabaseURL = null!;
		public string SupabaseAnonKey = null!;
	} 
}
