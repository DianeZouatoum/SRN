
Skip to content
This repository

    Pull requests
    Issues
    Gist

    @DianeZouatoum

0
0

    2

DianeZouatoum/SRN forked from lorendimitrov/SRN
Code
Pull requests 0
Projects 0
Wiki
Pulse
Graphs
Settings
SRN/ContactCard.cs
2f52bc2 10 minutes ago
@DianeZouatoum DianeZouatoum Update ContactCard.cs
@DianeZouatoum
@lorendimitrov
105 lines (85 sloc) 2.19 KB
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
          this.publicKey=new System.Byte[1024];    // Die Länge habe ich willkürlich ausgewählt
          this.publicKey= publicKey;
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
      
         using(StreamReader reader= new StreamReader(fileName))
         {
           System.String my_publicKey;
           this.contactName= reader.ReadLine();
           my_publicKey=reader.ReadLine();
           this.publicKey= Encoding.ASCII.GetBytes(my_publicKey);
           
           
         }
      }
      
      /// <summary>
      /// 
      /// </summary>
      /// <param name="fileName"></param>
      public void SaveToFile(System.String fileName)
      {
        throw new System.NotImplementedException();
        
        using(StreamWriter writer= new Streamwriter(fileName))
        {
           // writer.Writer(this.contactName);
              writer.WriteLine(this.contactName);
            // writer.Writer(publicKey.ToString());
             writer.WriteLine(publicKey.ToString());
        }
      }
    
    #endregion
  }
}

    Contact GitHub API Training Shop Blog About 

    © 2017 GitHub, Inc. Terms Privacy Security Status Help 

