namespace FCrypt
{
  /// <summary>
  /// 
  /// </summary>
  public class TrustedKey
  {
    #region Attributes
    
      /// <summary>
      /// 
      /// </summary>
      private System.String contactName;
      
      /// <summary>
      /// 
      /// </summary>
      private System.Byte[] publicKeyHash;
      
      /// <summary>
      /// 
      /// </summary>
      private System.Byte[] hashSignature;
    
    #endregion

    #region Properties
    
      /// <summary>
      /// 
      /// </summary>
      public System.String ContactName 
      {
        get 
        {
          return contactName;
        }
      }
  
      /// <summary>
      /// 
      /// </summary>
      public System.Byte[] PublicKeyHash 
      {
        get 
        {
          return publicKeyHash;
        }
      }
  
      /// <summary>
      /// 
      /// </summary>
      public System.Byte[] HashSignature 
      {
        get 
        {
          return hashSignature;
        }
      }
    
    #endregion
    
    #region Constructors and Destructor
    
      /// <summary>
      /// 
      /// </summary>
      /// <param name="cCard"></param>
      public TrustedKey( ContactCard cCard )
      {
        throw new System.NotImplementedException();
      }
    
    #endregion
  }
}
