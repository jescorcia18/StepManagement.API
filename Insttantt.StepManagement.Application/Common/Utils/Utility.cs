using Insttantt.StepManagement.Application.Common.Interfaces.Utils;
using Insttantt.StepManagement.Domain.Entities;
using Insttantt.StepManagement.Domain.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Insttantt.StepManagement.Application.Common.Utils
{
    public class Utility : IUtility
    {
        #region Global variables
        private readonly IConfiguration _configuration;
        #endregion

        #region Constructor
        public Utility(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion

        #region public Methods
        public async Task<string> Decrypt(string key, string cipherText)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(cipherText);

            var keyDecode = Convert.FromBase64String(key);
            key = Encoding.UTF8.GetString(keyDecode);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            var decrypt = await streamReader.ReadToEndAsync();

                            var decryptConn = Convert.FromBase64String(decrypt);
                            return Encoding.UTF8.GetString(decryptConn);
                        }
                    }
                }
            }
        }

        public async Task<string> Encrypt(string key, string data)
        {
            byte[] iv = new byte[16];
            byte[] array;
            if (string.IsNullOrEmpty(key)) throw new Exception("EncryptKey is invalid");
            var keyDecode = Convert.FromBase64String(key);
            var _key = Encoding.UTF8.GetString(keyDecode);
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(_key);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(data);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return await Task.FromResult(Convert.ToBase64String(array));
        }

        public async Task<IEnumerable<StepResponse>> MapToStepResponse(IEnumerable<Step> step)
        {
            return await Task.FromResult(step.Select(f => new StepResponse
            {
                StepId = f.StepId,
                StepName = f.StepName,
                StepDescription = f.StepDescription!,
                RequestType = f.RequestType!,
                ParameterType = f.ParameterType!,
                StepFieldsList = f.StepFields!.Select(sf => new StepFieldsResponse
                {
                    StepFieldId = sf.StepFieldId,
                    StepId = sf.StepId,
                    FieldId = sf.StepId,
                    InputOuput = sf.InputOutput
                }).ToList()
            }));
        }

        public async Task<StepResponse> MapToStepResponse(Step step)
        {
            if (step == null) throw new ArgumentNullException(nameof(step));

            return await Task.FromResult(new StepResponse
            {
                StepId = step.StepId,
                StepName = step.StepName,
                StepDescription = step.StepDescription!,
                RequestType = step.RequestType!,
                ParameterType = step.ParameterType!,
                StepFieldsList = step.StepFields!.Select(sf => new StepFieldsResponse
                {
                    StepFieldId = sf.StepFieldId,
                    StepId = sf.StepId,
                    FieldId = sf.StepId,
                    InputOuput = sf.InputOutput
                }).ToList()
            });
        }

        public async Task<StepFieldsResponse> MapToStepFieldsResponse(StepFields stepFields)
        {
            return await Task.FromResult(new StepFieldsResponse
            {
                StepFieldId = stepFields.StepFieldId,
                StepId = stepFields.StepId,
                FieldId = stepFields.FieldId,
                InputOuput = stepFields.InputOutput
            });
        }

        public async Task<IEnumerable<StepFieldsResponse>> MapToStepFieldsResponse(IEnumerable<StepFields> step)
        {
            return await Task.FromResult(step.Select(f => new StepFieldsResponse
            {
                StepFieldId = f.StepFieldId,
                StepId = f.StepId,
                FieldId = f.FieldId,
                InputOuput = f.InputOutput
            }));
        }

        public async Task<StepFieldsRequest> MapToStepFieldsRequest(StepFieldsResponse stepFields)
        {
            return await Task.FromResult(new StepFieldsRequest
            {
                StepId = stepFields.StepId,
                FieldId = stepFields.FieldId,
                InputOuput = stepFields.InputOuput
            });
        }
        #endregion
    }
}
