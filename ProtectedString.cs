namespace FCrypt
{
  /// <summary>
  /// 
  /// </summary>
  public class ProtectedString : System.IDisposable
  {
    #region Attributes
    
      /// <summary>
      /// 
      /// </summary>
      private System.Byte[] data;
      
      /// <summary>
      /// 
      /// </summary>
      private System.Boolean isProtected;
      
      /// <summary>
      /// 
      /// </summary>
      private System.Int32 size;

    #endregion
    
    #region Properties 

      /// <summary>
      /// 
      /// </summary>
      public System.Boolean IsProtected 
      {
        get 
        {
          return this.isProtected;
        }
      }
      
      /// <summary>
      /// 
      /// </summary>
      public System.Int32 Size
      {
        get
        {
          return this.size;
        }
      }

    #endregion
    
    #region Exceptions
    
      /// <summary>
      /// 
      /// </summary>
      public class StringProtectedException : System.Exception, System.Runtime.Serialization.ISerializable
      {
        #region Constructors 
        
          /// <summary>
          /// 
          /// </summary>
          public StringProtectedException()
          {
          }
      
          /// <summary>
          /// 
          /// </summary>
          /// <param name="message"></param>
          public StringProtectedException(System.String message) : base(message)
          {
          }
      
          /// <summary>
          /// 
          /// </summary>
          /// <param name="message"></param>
          /// <param name="innerException"></param>
          public StringProtectedException(System.String message, System.Exception innerException) : base(message, innerException)
          {
          }
      
          /// <summary>
          /// This constructor is needed for serialization.
          /// </summary>
          /// <param name="info"></param>
          /// <param name="context"></param>
          protected StringProtectedException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
          {
          }
          
        #endregion
      }
    
    #endregion

    #region Constructors
    
      /// <summary>
      /// 
      /// </summary>
      /// <param name="size"></param>
      public ProtectedString(System.Int32 size)
      {
        #region Check requested memory size is smaller or equal zero 
        
          if( size <= 0 )
          {
            throw new System.ArgumentOutOfRangeException();
          }
        
        #endregion
        
        #region Check requested memory size is a multiple of 16

          if( size % 16 != 0 )
          {
            throw new System.ArgumentOutOfRangeException();
          }

        #endregion
        
        #region Initialize attributes
     
          this.data = new System.Byte[size];
          this.isProtected = false;
          this.size = size;

        #endregion
        
        #region Intialy protect memory
        
          this.Protect();
        
        #endregion
      }
    
      /// <summary>
      /// 
      /// </summary>
      ~ProtectedString()
      {
        // Dispose memory
        this.Dispose();
      }
      
    #endregion
    
    #region Methodes

      /// <summary>
      /// 
      /// </summary>
      public void Protect()
      {
        #region Check memory is already protected

          if( this.isProtected )
          {
            throw new System.InvalidOperationException();
          }
          
        #endregion

        #region Protect memory

          try
          {
            // Encrypt memory and prevent decryption by other processes
            System.Security.Cryptography.ProtectedMemory.Protect( this.data, System.Security.Cryptography.MemoryProtectionScope.SameProcess );
            
            // Mark memory internaly as protected
            this.isProtected = true;
          }
          catch( System.Exception e )
          {
            // A errors is occured
            // We have to suppose the memory is not protected
            
            // Mark memory internaly as unprotected
            this.isProtected = false;
            
            // Re-throw occured error
            throw e;
          }

        #endregion
      }
      
      /// <summary>
      /// 
      /// </summary>
      public void Unprotect()
      {
        #region Check memory is already unprotected

          if( !this.isProtected )
          {
            throw new System.InvalidOperationException();
          }
          
        #endregion

        #region Unprotect memory

          try
          {
            // Decrypt memory
            System.Security.Cryptography.ProtectedMemory.Unprotect( this.data, System.Security.Cryptography.MemoryProtectionScope.SameProcess );
            
            // Mark memory internaly as unprotected
            this.isProtected = false;
          }
          catch( System.Exception e )
          {
            // A errors is occured
            // We have to suppose the memory is still protected
            
            // Mark memory internaly as protected
            this.isProtected = true;
            
            // Re-throw occured error
            throw e;
          }

        #endregion
      }
      
      /// <summary>
      /// 
      /// </summary>
      /// <returns></returns>
      public System.Char[] GetChars()
      {
        #region Check memory is protected
          
          if( this.isProtected )
          {
            throw new StringProtectedException();
          }
          
        #endregion
        
        return System.Text.Encoding.ASCII.GetChars( this.data );
      }
      
      /// <summary>
      /// 
      /// </summary>
      /// <returns></returns>
      public System.Byte[] GetBytes()
      {
        #region Check memory is protected
          
          if( this.isProtected )
          {
            throw new StringProtectedException();
          }
          
        #endregion
        
        return this.data;
      }
      
      /// <summary>
      /// 
      /// </summary>
      /// <param name="position"></param>
      /// <param name="character"></param>
      public void SetChar(System.Int32 position, System.Char character)
      {
        #region Check position is invalid
        
          if( ( position >= this.size ) || ( position < 0 ) )
          {
            throw new System.ArgumentOutOfRangeException();
          }
        
        #endregion
        
        #region Check memory is protected
        
          if( this.isProtected )
          {
            throw new StringProtectedException();
          }
        
        #endregion
        
        #region Set char
        
          this.data[position] = ((System.Byte) character);
        
        #endregion
      }

    #endregion

    #region IDisposable implementation

      /// <summary>
      /// 
      /// </summary>
      public void Dispose()
      {
        #region Free memory
        
          // encrypt memory
          if( !this.isProtected )
          {
            this.Protect();
          }
          
          // free memory
          this.data = null;
          System.GC.Collect();
          
        #endregion
      }
  
    #endregion
  }
}