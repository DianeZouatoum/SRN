namespace FCrypt
{
  /// <summary>
  /// 
  /// </summary>
  public class ProtectedMemory: System.IDisposable
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
      private System.Int32 sizeInByte;

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
      public System.Int32 SizeInByte
      {
        get
        {
          return this.sizeInByte;
        }
      }

    #endregion
    
    #region Exceptions
    
      /// <summary>
      /// 
      /// </summary>
      public class MemoryProtectedException : System.Exception, System.Runtime.Serialization.ISerializable
      {
        #region Constructors 
        
          /// <summary>
          /// 
          /// </summary>
          public MemoryProtectedException()
          {
          }
      
          /// <summary>
          /// 
          /// </summary>
          /// <param name="message"></param>
          public MemoryProtectedException(System.String message) : base(message)
          {
          }
      
          /// <summary>
          /// 
          /// </summary>
          /// <param name="message"></param>
          /// <param name="innerException"></param>
          public MemoryProtectedException(System.String message, System.Exception innerException) : base(message, innerException)
          {
          }
      
          /// <summary>
          /// This constructor is needed for serialization.
          /// </summary>
          /// <param name="info"></param>
          /// <param name="context"></param>
          protected MemoryProtectedException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
          {
          }
          
        #endregion
      }
    
    #endregion

    #region Constructors
    
      /// <summary>
      /// 
      /// </summary>
      /// <param name="sizeInByte"></param>
      public ProtectedMemory(System.Int32 sizeInByte)
      {
        #region Check requested memory size is smaller or equal zero 
        
          if( sizeInByte <= 0 )
          {
            throw new System.ArgumentOutOfRangeException();
          }
        
        #endregion
        
        #region Check requested memory size is a multiple of 16

          if( sizeInByte % 16 != 0 )
          {
            throw new System.ArgumentOutOfRangeException();
          }

        #endregion
        
        #region Initialize attributes
     
          this.data = new System.Byte[sizeInByte];
          this.sizeInByte = sizeInByte;
          this.isProtected = false;

        #endregion
        
        #region Intialy protect memory
        
          this.Protect();
        
        #endregion
      }
    
      /// <summary>
      /// 
      /// </summary>
      ~ProtectedMemory()
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
      public System.Byte[] GetBytes()
      {
        #region Check memory is protected
          
          if( this.isProtected )
          {
            throw new MemoryProtectedException();
          }
          
        #endregion
        
        return this.data;
      }
      
      /// <summary>
      /// 
      /// </summary>
      /// <param name="position"></param>
      /// <param name="data"></param>
      public void SetByte(System.Int32 position, System.Byte data)
      {
        #region Check position is invalid
        
          if( ( position >= this.sizeInByte ) || ( position < 0 ) )
          {
            throw new System.ArgumentOutOfRangeException();
          }
        
        #endregion
        
        #region Check memory is protected
        
          if( this.isProtected )
          {
            throw new MemoryProtectedException();
          }
        
        #endregion
        
        #region Set char
        
          this.data[position] = data;
        
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