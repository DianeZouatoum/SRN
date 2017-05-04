namespace FCrypt
{
  /// <summary>
  /// 
  /// </summary>
  public class ContactCard
  {
    #region Attributes

      /// <summary>
      /// 
      /// </summary>
      private System.String contactName;
      
      /// <summary>
      /// 
      /// </summary>
      private System.Byte[] publicKey;

    #endregion
    
    #region Properties
    
      /// <summary>
      /// 
      /// </summary>
      public System.String ContactName 
      {
        get 
        {
          return this.contactName;
        }
      }
  
      /// <summary>
      /// 
      /// </summary>
      public System.Byte[] PublicKey 
      {
        get 
        {
          return this.publicKey;
        }
      }
    
    #endregion
    
    #region Constructors and Destructor
    
      /// <summary>
      /// 
      /// </summary>
      /// <param name="contactName"></param>
      /// <param name="publicKey"></param>
      public ContactCard( System.String contactName, System.Byte[] publicKey )
      {    
          throw new System.NotImplementedException();
          this.contactName=contactName;
          this.publicKey=publicKey;
      }
      
    #endregion
    
    #region Methodes
    
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
       // System.IO.StreamWriter file =new System.IO.StreamWriter(fileName, true);
        //file.WriteLine( contactName);
        // file.WriteLine( publicKey);
      }
    
    #endregion
  }
}
