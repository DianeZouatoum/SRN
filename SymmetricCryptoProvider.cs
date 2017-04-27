namespace FCrypt
{
  /// <summary>
  /// 
  /// </summary>
  public static class SymmetricCryptoProvider
  {
    #region Exceptions
    
      /// <summary>
      /// 
      /// </summary>
      public class UnsecureException : System.Exception, System.Runtime.Serialization.ISerializable
      {
        #region Constructors 
        
          /// <summary>
          /// 
          /// </summary>
          public UnsecureException()
          {
          }
      
          /// <summary>
          /// 
          /// </summary>
          /// <param name="message"></param>
          public UnsecureException(System.String message) : base(message)
          {
          }
      
          /// <summary>
          /// 
          /// </summary>
          /// <param name="message"></param>
          /// <param name="innerException"></param>
          public UnsecureException(System.String message, System.Exception innerException) : base(message, innerException)
          {
          }
      
          /// <summary>
          /// This constructor is needed for serialization.
          /// </summary>
          /// <param name="info"></param>
          /// <param name="context"></param>
          protected UnsecureException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
          {
          }
          
        #endregion
      }
    
    #endregion

    #region Methodes
    
      /// <summary>
      /// 
      /// </summary>
      /// <param name="secretKey"></param>
      /// <param name="plainData"></param>
      /// <returns></returns>
      public static ProtectedMemory EncryptData(ProtectedString secretKey, ProtectedMemory plainData)
      {
        ProtectedMemory returnValue = null;
        System.Byte[] encryptedData = null;
        
        #region Check protection of secret key
        
          if( !secretKey.IsProtected )
          {
            throw new UnsecureException();
          }
        
        #endregion
        
        #region Check protection of plain data
        
          if( !plainData.IsProtected )
          {
            throw new UnsecureException();
          }
        
        #endregion
        
        #region Prepare encryption provider
        
          // Unprotect memory containing secret key
          secretKey.Unprotect();
        
          // Create encryption provider
          System.Security.Cryptography.SymmetricAlgorithm encryptionProvider = System.Security.Cryptography.Aes.Create();
          encryptionProvider.Mode = System.Security.Cryptography.CipherMode.CBC;
          encryptionProvider.Key = secretKey.GetBytes();
          encryptionProvider.GenerateIV();
        
          // Reprotect memory containing secret key
          secretKey.Protect();
          
        #endregion
        
        // Create encryptor
        System.Security.Cryptography.ICryptoTransform encryptor = encryptionProvider.CreateEncryptor( encryptionProvider.Key, encryptionProvider.IV );
        
        // Create handle to stream data into memory
        using( System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
        {
          // Write IV to temp memory (IV length is static => 16 )
          memoryStream.Write( encryptionProvider.IV, 0, 16 );
          
          // Create handle for data encryption; data streamed to this stream will be automatically encrypted and streamed to memory
          using( System.Security.Cryptography.CryptoStream cryptoStream = new System.Security.Cryptography.CryptoStream( memoryStream, encryptor, System.Security.Cryptography.CryptoStreamMode.Write ) )
          {
            // Create handle to write data to a stream; data written to this stream will be automatically encrypted and streamed to memory
            using ( System.IO.StreamWriter streamWriter = new System.IO.StreamWriter( cryptoStream ) )
            {
              // Unprotect plain data
              plainData.Unprotect();
              
              #region Write and encrypt plain data to temp memory
              
                foreach( System.Byte b in plainData.GetBytes() )
                {
                  streamWriter.Write( (System.Char) b );
                }
              
              #endregion
              
              // Reprotect plain data
              plainData.Protect();
            }
          }
          
          // Save content of temp memory in temp buffer
          encryptedData = memoryStream.ToArray();
        }
        
        // Dispose encryptor
        encryptor.Dispose();
        
        // Dispose encryption provider
        encryptionProvider.Dispose();
        
        #region Save cyphered data in protected memory
        
          // Create protected memory for cyphered data
          returnValue = new ProtectedMemory( encryptedData.Length );
        
          // Unprotect memory for cyphered data
          returnValue.Unprotect();
             
          // Copy cyphered data in encrypted memory        
          for( System.Int32 i = 0; i<encryptedData.Length; i++ )
          {
            returnValue.SetByte( i, encryptedData[i] );
          }
              
          // Reprotect memory with cyphered data
          returnValue.Protect();
              
        #endregion
        
        return returnValue;
      }
    
      /// <summary>
      /// 
      /// </summary>
      /// <param name="secretKey"></param>
      /// <param name="cypheredData"></param>
      /// <returns></returns>
      public static ProtectedMemory DecryptData(ProtectedString secretKey, ProtectedMemory cypheredData)
      {
        ProtectedMemory returnValue = null;
        System.Byte[] encryptedData = null;
        System.Byte[] ivData = null;
        
        #region Check protection of secret key
        
          if( !secretKey.IsProtected )
          {
            throw new UnsecureException();
          }
        
        #endregion
        
        #region Check protection of cyphered data
        
          if( !cypheredData.IsProtected )
          {
            throw new UnsecureException();
          }
        
        #endregion
        
        #region Split cyphered data
        
          // Unprotect cyphered memory
          cypheredData.Unprotect();
          
          // extract iv data (IV length is static => 16)
          ivData = new System.Byte[16];
          System.Array.Copy( cypheredData.GetBytes(), 0, ivData, 0, 16 );
          
          // extract encrypted data
          encryptedData = new System.Byte[cypheredData.SizeInByte - 16];
          System.Array.Copy( cypheredData.GetBytes(), 16, encryptedData, 0, (cypheredData.SizeInByte - 16) );
          
          // Reprotect cyphered memory
          cypheredData.Protect();
        
        #endregion
        
        #region Prepare encryption provider
        
          // Unprotect memory containing secret key
          secretKey.Unprotect();
        
          // Create encryption provider
          System.Security.Cryptography.SymmetricAlgorithm encryptionProvider = System.Security.Cryptography.Aes.Create();
          encryptionProvider.Mode = System.Security.Cryptography.CipherMode.CBC;
          encryptionProvider.Key = secretKey.GetBytes();
          encryptionProvider.IV = ivData;
        
          // Reprotect memory containing secret key
          secretKey.Protect();
          
        #endregion
        
        // Create decryptor
        System.Security.Cryptography.ICryptoTransform decryptor = encryptionProvider.CreateDecryptor( encryptionProvider.Key, encryptionProvider.IV );
        
        // Create handle to memory of encrypted data
        using( System.IO.MemoryStream memoryStream = new System.IO.MemoryStream( encryptedData ))
        {
          // Create handle for data decryption; data streamed by this stream will be automatically decrypted
          using( System.Security.Cryptography.CryptoStream cryptoStream = new System.Security.Cryptography.CryptoStream( memoryStream, decryptor, System.Security.Cryptography.CryptoStreamMode.Read ) )
          {
            // Create handle to read data of a strea,; data readed by this stream will be automatically decrypted
            using ( System.IO.StreamReader streamReader = new System.IO.StreamReader( cryptoStream ) )
            {
              #region Save plain data in protected memory
              
                // Save plain data in temp buffer
                System.String plainData = streamReader.ReadToEnd();
              
                // Create protected memory for plain data
                returnValue = new ProtectedMemory( plainData.Length );
              
                // Unprotect memory for plain data
                returnValue.Unprotect();
                   
                // Copy plain data in encrypted memory        
                for( System.Int32 i = 0; i<plainData.Length; i++ )
                {
                  returnValue.SetByte( i, (System.Byte) plainData[i] );
                }
                    
                // Reprotect memory with plain data
                returnValue.Protect();
                
                // Save erase temp buffer
                plainData = null;
                System.GC.Collect();
                
              #endregion
            }
          }
        }
        
        // Dispose decryptor
        decryptor.Dispose();
        
        // Dispose encryption provider
        encryptionProvider.Dispose();
                
        return returnValue;
      }
    
    #endregion
  }
}
