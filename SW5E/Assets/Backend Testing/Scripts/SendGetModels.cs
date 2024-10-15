using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using Postgrest.Models;
using Postgrest.Responses;
using Postgrest.Attributes;

namespace Brett.Experiments.Supabase
{
    // https://supabase.com/docs/reference/csharp/installing
    [Table("tests")]
    public class Tests : BaseModel
    {
        [PrimaryKey("id", false)]
        public int id { get; set; }

        // created_at is setup to not be nullable, so we need to include the extra piece or we'll get errors when inserting
        [Column("created_at", Newtonsoft.Json.NullValueHandling.Ignore)]
        public string createdAt { get; set; }

        [Column("body")]
        public string body { get; set; }

        [Column("number")]
        public int number { get; set; }

        public override string ToString()
        {
            return $"{{ \"id\": \"{id}\", \"created_at\": \"{createdAt}\", \"body\": \"{body}\", \"number\": \"{number}\"}},";
        }
    }

    public class SendGetModels : MonoBehaviour
    {
        public TMP_InputField bodyInput = null;
        public TMP_InputField numberInput = null;
        public TMP_Text errorText = null;
        public TMP_Text resultsText = null;
        public SupabaseManager supabaseManager = null;

        public void SendData()
        {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            SendData(bodyInput.text, int.Parse(numberInput.text));
#pragma warning restore CS4014
        }

        // https://supabase.com/docs/reference/csharp/select
        private async Task SendData(string body, int number)
        {
            // Create a model to insert into the database
            Tests model = new Tests
            {
                body = body,
                number = number
            };

            // Insert the model
            await supabaseManager.Supabase().From<Tests>().Insert(model);
            Debug.Log("Data inserted");
        }

        public void GetData()
        {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            FetchData();
#pragma warning restore CS4014
        }

        // https://supabase.com/docs/reference/csharp/insert
        private async Task FetchData()
        {
            // Fetch the models in the table to get data as a list of objects
            ModeledResponse<Tests> result = await supabaseManager.Supabase().From<Tests>().Get();
            string results = "[";
            result.Models.ForEach((x) => { results += x.ToString() + "\n"; });
            results += "]";
            Debug.Log(results);
            resultsText.text = results;
        }
    }
}