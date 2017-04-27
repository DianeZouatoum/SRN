namespace FCrypt
{
  /// <summary>
  /// 
  /// </summary>
  public class KeyBox
  {
    #region Attributes
    
      /// <summary>
      /// 
      /// </summary>
      private System.String ownerName;
      
      /// <summary>
      /// 
      /// </summary>
      private System.Byte[] cypheredPrivateKey;
      
      /// <summary>
      /// 
      /// </summary>
      private System.Byte[] publicKey;
      
      /// <summary>
      /// 
      /// </summary>
      private System.Collections.Generic.List<TrustedKey> trustedKeys;

    #endregion
    
    #region Properties
    
      /// <summary>
      /// 
      /// </summary>
      public System.String OwnerName 
      {
        get 
        {
          return ownerName;
        }
      }
  
      /// <summary>
      /// 
      /// </summary>
      public System.Byte[] PublicKey 
      {
        get
        {
          return publicKey;
        }
      }
  
      /// <summary>
      /// 
      /// </summary>
      public System.Collections.Generic.IList<TrustedKey> TrustedKeys 
      {
        get 
        {
          return trustedKeys;
        }
      }
    
    #endregion
    
    #region Constructors and Destructor
    
    #endregion
    
    #region Methodes
    
      /// <summary>
      /// 
      /// </summary>
      /// <returns></returns>
      public ProtectedMemory GetPrivateKey()
      {
        throw new System.NotImplementedException();
      }
      
      /// <summary>
      /// 
      /// </summary>
      /// <returns></returns>
      public ContactCard GetMyContactCard()
      {
        return new ContactCard(this.ownerName, this.publicKey);
      }
      
      /// <summary>
      /// 
      /// </summary>
      /// <param name="cCard"></param>
      public void AddTrustedKey(ContactCard cCard)
      {
        throw new System.NotImplementedException();
      }
      
      /// <summary>
      /// 
      /// </summary>
      /// <param name="tKey"></param>
      public void RemoveTrustedKey(TrustedKey tKey)
      {
        throw new System.NotImplementedException();
      }
      
      /// <summary>
      /// 
      /// </summary>
      /// <param name="fileName"></param>
      public void LoadFromFile(System.String fileName)
      {
        throw new System.NotImplementedException();
      }
      
      /// <summary>
      /// 
      /// </summary>
      /// <param name="fileName"></param>
      public void SaveToFile(System.String fileName)
      {
        throw new System.NotImplementedException();
      }
    
    #endregion
  }
  
}
