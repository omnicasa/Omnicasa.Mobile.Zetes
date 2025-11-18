using System;
using Foundation;
using ObjCRuntime;

namespace Omnicasa.Mobile.Zetes.iOS
{

	// @protocol ReaderProtocol <NSObject>
	/*
	  Check whether adding [Model] to this declaration is appropriate.
	  [Model] is used to generate a C# class that implements this protocol,
	  and might be useful for protocols that consumers are supposed to implement,
	  since consumers can subclass the generated class instead of implementing
	  the generated interface. If consumers are not supposed to implement this
	  protocol, then [Model] is redundant and will generate code that will never
	  be used.
	*/
	[Protocol]
	[BaseType (typeof(NSObject))]
	interface ReaderProtocol
	{
		// @required -(NSString *)lastName;
		[Abstract]
		[Export ("lastName")]
		// [Verify (MethodToProperty)]
		string LastName { get; }

		// @required -(NSString *)firstName;
		[Abstract]
		[Export ("firstName")]
		//  [Verify (MethodToProperty)]
		string FirstName { get; }

		// @required -(NSString *)thirdName;
		[Abstract]
		[Export ("thirdName")]
		//  [Verify (MethodToProperty)]
		string ThirdName { get; }

		// @required -(NSString *)cardDeliveryMunicipality;
		[Abstract]
		[Export ("cardDeliveryMunicipality")]
		//  [Verify (MethodToProperty)]
		string CardDeliveryMunicipality { get; }

		// @required -(NSString *)cardNumber;
		[Abstract]
		[Export ("cardNumber")]
		//  [Verify (MethodToProperty)]
		string CardNumber { get; }

		// @required -(NSString *)cardValidFrom;
		[Abstract]
		[Export ("cardValidFrom")]
		//  [Verify (MethodToProperty)]
		string CardValidFrom { get; }

		// @required -(NSString *)cardValidTo;
		[Abstract]
		[Export ("cardValidTo")]
		//  [Verify (MethodToProperty)]
		string CardValidTo { get; }

		// @required -(NSString *)chipNumber;
		[Abstract]
		[Export ("chipNumber")]
		//  [Verify (MethodToProperty)]
		string ChipNumber { get; }

		// @required -(NSString *)nationality;
		[Abstract]
		[Export ("nationality")]
		//  [Verify (MethodToProperty)]
		string Nationality { get; }

		// @required -(NSString *)placeOfBirth;
		[Abstract]
		[Export ("placeOfBirth")]
		//  [Verify (MethodToProperty)]
		string PlaceOfBirth { get; }

		// @required -(NSString *)sex;
		[Abstract]
		[Export ("sex")]
		//  [Verify (MethodToProperty)]
		string Sex { get; }

		// @required -(NSString *)dateOfBirth;
		[Abstract]
		[Export ("dateOfBirth")]
		//  [Verify (MethodToProperty)]
		string DateOfBirth { get; }

		// @required -(NSDate *)NSDateOfBirth;
		[Abstract]
		[Export ("NSDateOfBirth")]
		//  [Verify (MethodToProperty)]
		NSDate NSDateOfBirth { get; }

		// @required -(NSString *)dateOfBirthWithFormat:(NSString *)format localIdentifier:(NSString *)localID;
		[Abstract]
		[Export ("dateOfBirthWithFormat:localIdentifier:")]
		string DateOfBirthWithFormat (string format, string localID);

		// @required -(NSString *)natNumber;
		[Abstract]
		[Export ("natNumber")]
		//  [Verify (MethodToProperty)]
		string NatNumber { get; }

		// @required -(NSString *)address;
		[Abstract]
		[Export ("address")]
		//  [Verify (MethodToProperty)]
		string Address { get; }

		// @required -(NSString *)postalCode;
		[Abstract]
		[Export ("postalCode")]
		//  [Verify (MethodToProperty)]
		string PostalCode { get; }

		// @required -(NSString *)municipality;
		[Abstract]
		[Export ("municipality")]
		// [Verify (MethodToProperty)]
		string Municipality { get; }

		// @required -(int)specialStatus;
		[Abstract]
		[Export ("specialStatus")]
		// [Verify (MethodToProperty)]
		int SpecialStatus { get; }

		// @required -(NSString *)nobleCondition;
		[Abstract]
		[Export ("nobleCondition")]
		// [Verify (MethodToProperty)]
		string NobleCondition { get; }

		// @required -(NSString *)specialOrganisation;
		[Abstract]
		[Export ("specialOrganisation")]
		// [Verify (MethodToProperty)]
		string SpecialOrganisation { get; }

		// @required -(NSString *)memberOfFamily;
		[Abstract]
		[Export ("memberOfFamily")]
		// [Verify (MethodToProperty)]
		string MemberOfFamily { get; }

		// @required -(int)duplicate;
		[Abstract]
		[Export ("duplicate")]
		// [Verify (MethodToProperty)]
		int Duplicate { get; }

		// @required -(NSData *)photoDigest;
		[Abstract]
		[Export ("photoDigest")]
		// [Verify (MethodToProperty)]
		NSData PhotoDigest { get; }

		// @required -(int)docTypeInt;
		[Abstract]
		[Export ("docTypeInt")]
		// [Verify (MethodToProperty)]
		int DocTypeInt { get; }

		// @required +(NSString *)docTypeToString:(int)i;
		[Static, Abstract]
		[Export ("docTypeToString:")]
		string DocTypeToString (int i);

		// @required -(NSString *)workPermitMention;
		[Abstract]
		[Export ("workPermitMention")]
		// [Verify (MethodToProperty)]
		string WorkPermitMention { get; }

		// @required -(NSString *)dateAndCountryOfProtection;
		[Abstract]
		[Export ("dateAndCountryOfProtection")]
		// [Verify (MethodToProperty)]
		string DateAndCountryOfProtection { get; }

		// @required -(NSString *)vatNumber1;
		[Abstract]
		[Export ("vatNumber1")]
		// [Verify (MethodToProperty)]
		string VatNumber1 { get; }

		// @required -(NSString *)vatNumber2;
		[Abstract]
		[Export ("vatNumber2")]
		// [Verify (MethodToProperty)]
		string VatNumber2 { get; }

		// @required -(NSString *)regionNumber;
		[Abstract]
		[Export ("regionNumber")]
		// [Verify (MethodToProperty)]
		string RegionNumber { get; }

		// @required -(NSData *)idFile;
		[Abstract]
		[Export ("idFile")]
		// [Verify (MethodToProperty)]
		NSData IdFile { get; }

		// @required -(NSData *)idSigFile;
		[Abstract]
		[Export ("idSigFile")]
		// [Verify (MethodToProperty)]
		NSData IdSigFile { get; }

		// @required -(NSData *)addressFile;
		[Abstract]
		[Export ("addressFile")]
		// [Verify (MethodToProperty)]
		NSData AddressFile { get; }

		// @required -(NSData *)addressSigFile;
		[Abstract]
		[Export ("addressSigFile")]
		// [Verify (MethodToProperty)]
		NSData AddressSigFile { get; }

		// @required -(NSData *)picture;
		[Abstract]
		[Export ("picture")]
		// [Verify (MethodToProperty)]
		NSData Picture { get; }

		// @required -(NSData *)authenticationCertificate;
		[Abstract]
		[Export ("authenticationCertificate")]
		// [Verify (MethodToProperty)]
		NSData AuthenticationCertificate { get; }

		// @required -(NSData *)nonrepudiationCertificate;
		[Abstract]
		[Export ("nonrepudiationCertificate")]
		// [Verify (MethodToProperty)]
		NSData NonrepudiationCertificate { get; }

		// @required -(NSData *)caCertificate;
		[Abstract]
		[Export ("caCertificate")]
		// [Verify (MethodToProperty)]
		NSData CaCertificate { get; }

		// @required -(NSData *)rootCaCertificate;
		[Abstract]
		[Export ("rootCaCertificate")]
		// [Verify (MethodToProperty)]
		NSData RootCaCertificate { get; }

		// @required -(NSData *)rrnCertificate;
		[Abstract]
		[Export ("rrnCertificate")]
		// [Verify (MethodToProperty)]
		NSData RrnCertificate { get; }
	}

	// @protocol ReaderDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface ReaderDelegate
	{
		// @optional -(void)readerDidChange:(BOOL)attached;
		[Export ("readerDidChange:")]
		void ReaderDidChange (bool attached);

		// @optional -(void)cardDidChange:(BOOL)attached;
		[Export ("cardDidChange:")]
		void CardDidChange (bool attached);

		// @optional -(void)didDetectReader:(NSString *)reader;
		[Export ("didDetectReader:")]
		void DidDetectReader (string reader);
	}

	// @interface Reader : NSObject <ReaderProtocol>
	[BaseType (typeof(NSObject))]
	interface Reader
	{
		// -(id)initWithPreferredReader:(NSString *)name OEM:(NSString *)oemVersion;
		[Export ("initWithPreferredReader:OEM:")]
		IntPtr Constructor (string name, string oemVersion);

		// -(id)initWithPreferredReader:(NSString *)name OEM:(NSString *)oemVersion type:(NSString *)type;
		[Export ("initWithPreferredReader:OEM:type:")]
		IntPtr Constructor (string name, string oemVersion, string type);

		// -(void)startScan;
		[Export ("startScan")]
		void StartScan ();

		// -(void)stopScan;
		[Export ("stopScan")]
		void StopScan ();

		// -(int)open;
		[Export ("open")]
		// [Verify (MethodToProperty)]
		int Open { get; }

		// -(void)close;
		[Export ("close")]
		void Close ();

		// -(void)beginTransaction;
		[Export ("beginTransaction")]
		void BeginTransaction ();

		// -(void)endTransaction;
		[Export ("endTransaction")]
		void EndTransaction ();

		// -(BOOL)supportsPinPad;
		[Export ("supportsPinPad")]
		// [Verify (MethodToProperty)]
		bool SupportsPinPad { get; }

		// -(NSString *)version;
		[Export ("version")]
		// [Verify (MethodToProperty)]
		string Version { get; }

		[Wrap ("WeakDelegate")]
		ReaderDelegate Delegate { get; set; }

		// @property (nonatomic, weak) id<ReaderDelegate> delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		NSObject WeakDelegate { get; set; }
	}

	// @protocol SignatureProtocol <NSObject>
	/*
	  Check whether adding [Model] to this declaration is appropriate.
	  [Model] is used to generate a C# class that implements this protocol,
	  and might be useful for protocols that consumers are supposed to implement,
	  since consumers can subclass the generated class instead of implementing
	  the generated interface. If consumers are not supposed to implement this
	  protocol, then [Model] is redundant and will generate code that will never
	  be used.
	*/
	[Protocol]
	[BaseType (typeof(NSObject))]
	interface SignatureProtocol
	{
		// @required -(int)verifyPin:(NSData *)pinData forSignatureType:(SignatureType)signatureType;
		[Abstract]
		[Export ("verifyPin:forSignatureType:")]
		int VerifyPin (NSData pinData, SignatureType signatureType);

		// @required -(int)pkcs1Sign:(NSData *)hash digestAlgo:(NSInteger)digestAlgo signature:(NSMutableData *)signature;
		[Abstract]
		[Export ("pkcs1Sign:digestAlgo:signature:")]
		int Pkcs1Sign (NSData hash, nint digestAlgo, NSMutableData signature);

		// @required -(int)sign:(NSData *)hash digestAlgo:(NSInteger)digestAlgo signature:(NSMutableData *)signature;
		[Abstract]
		[Export ("sign:digestAlgo:signature:")]
		int Sign (NSData hash, nint digestAlgo, NSMutableData signature);

		// @required -(int)cmsSign:(NSData *)data digestAlgo:(NSInteger)digestAlgo isDetached:(BOOL)detached cms:(NSMutableData *)cms;
		[Abstract]
		[Export ("cmsSign:digestAlgo:isDetached:cms:")]
		int CmsSign (NSData data, nint digestAlgo, bool detached, NSMutableData cms);
	}

	// @interface  (Reader) <SignatureProtocol>
	[Category]
	[BaseType (typeof(Reader))]
	interface Reader_
	{
	}

	// @interface eID_SDK : NSObject
	[BaseType (typeof(NSObject))]
	interface eID_SDK
	{
		// +(NSString *)sdkVersion;
		[Static]
		[Export ("sdkVersion")]
		// [Verify (MethodToProperty)]
		string SdkVersion { get; }

		// +(NSString *)sdkReleaseDate;
		[Static]
		[Export ("sdkReleaseDate")]
		// [Verify (MethodToProperty)]
		string SdkReleaseDate { get; }
	}
}
