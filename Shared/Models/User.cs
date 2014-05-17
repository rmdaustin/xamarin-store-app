using System;
using System.Threading.Tasks;

namespace XamarinStore
{
	public class User
	{
		public User ()
		{

		}

		// callback to send corrected state spelling suggestion back to caller
		public event Action<string> StateCorrected = delegate {};

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Email { get; set; }

		public string Token { get; set; }

		public string Phone {get;set;}

		public string Address { get; set; }

		public string Address2 { get; set; }

		public string City { get; set; }

		public string State { get; set; }

		public string Country { get; set; }

		public string ZipCode { get; set; }

		public async Task<Tuple<bool,string>> IsInformationValid()
		{

			if (string.IsNullOrEmpty (FirstName))
				return new Tuple<bool, string>(false,"First name is required");

			if (string.IsNullOrEmpty (LastName))
				return new Tuple<bool, string>(false,"Last name is required");

			if (string.IsNullOrEmpty (Phone))
				return new Tuple<bool, string>(false,"Phone number is required");

			if (string.IsNullOrEmpty (FirstName))
				return new Tuple<bool, string>(false,"First name is required");

			if (string.IsNullOrEmpty (LastName))
				return new Tuple<bool, string>(false,"Last name is required");

			if(string.IsNullOrEmpty(Address))
				return new Tuple<bool, string>(false,"Address is required");

			if (string.IsNullOrEmpty (City))
				return new Tuple<bool, string>(false,"City is required");

			if (Country.ToLower () == "usa") {
				// If they entered a USPS State abbreviation, correct it to the spelled-out version and confirm
				var StateWasCorrected = StateCorrectedFromPostalAbbrev (State);
				if (StateWasCorrected.Item1) {
					State = StateWasCorrected.Item2;
					StateCorrected (State);
					return new Tuple<bool, string> (false, "We expanded your State abbreviation.  Please confirm and submit again.");
				}
				var states = await WebService.Shared.GetStates (await WebService.Shared.GetCountryFromCode(Country));
				if(!states.Contains(State))
					return new Tuple<bool, string> (false, "State is required");
			}

			if (string.IsNullOrEmpty (Country))
				return new Tuple<bool, string>(false,"Country is required");

			if (string.IsNullOrEmpty (ZipCode))
				return new Tuple<bool, string>(false,"ZipCode is required");

			return new Tuple<bool, string>(true,"");
		}
		/// <summary>
		/// Tries to return a spelled-out US state from the two-letter abbreviation
		/// </summary>
		/// <returns>Tuple: true if changes we made, false if no changes were made; corrected or original State</returns>
		/// <param name="State">State suspected of being an abbreviation</param>
		private Tuple<bool,string> StateCorrectedFromPostalAbbrev (string State) {
			bool WasCorrected = true;
			switch (State.Trim().ToUpper()) {
			// Some of these are not currently supported by the store (e.g. PR->Puerto Rico), so fail later validation,
			// but leaving them in for when the web service gets updated for U.S. territories
			case "AL" : 
				State = "Alabama"; 
				break;
			case "AK" : 
				State = "Alaska"; 
				break;
			case "AS" : 
				State = "American Samoa"; 
				break;
			case "AZ" : 
				State = "Arizona"; 
				break;
			case "AR" : 
				State = "Arkansas"; 
				break;
			case "CA" : 
				State = "California"; 
				break;
			case "CO" : 
				State = "Colorado"; 
				break;
			case "CT" : 
				State = "Connecticut"; 
				break;
			case "DE" : 
				State = "Delaware"; 
				break;
			case "DC" : 
				State = "District of Columbia"; 
				break;
			case "FM" : 
				State = "Federated States of Micronesia"; 
				break;
			case "FL" : 
				State = "Florida"; 
				break;
			case "GA" : 
				State = "Georgia"; 
				break;
			case "GU" : 
				State = "Guam"; 
				break;
			case "HI" : 
				State = "Hawaii"; 
				break;
			case "ID" : 
				State = "Idaho"; 
				break;
			case "IL" : 
				State = "Illinois"; 
				break;
			case "IN" : 
				State = "Indiana"; 
				break;
			case "IA" : 
				State = "Iowa"; 
				break;
			case "KS" : 
				State = "Kansas"; 
				break;
			case "KY" : 
				State = "Kentucky"; 
				break;
			case "LA" : 
				State = "Louisiana"; 
				break;
			case "ME" : 
				State = "Maine"; 
				break;
			case "MH" : 
				State = "Marshall Islands"; 
				break;
			case "MD" : 
				State = "Maryland"; 
				break;
			case "MA" : 
				State = "Massachusetts"; 
				break;
			case "MI" : 
				State = "Michigan"; 
				break;
			case "MN" : 
				State = "Minnesota"; 
				break;
			case "MS" : 
				State = "Mississippi"; 
				break;
			case "MO" : 
				State = "Missouri"; 
				break;
			case "MT" : 
				State = "Montana"; 
				break;
			case "NE" : 
				State = "Nebraska"; 
				break;
			case "NV" : 
				State = "Nevada"; 
				break;
			case "NH" : 
				State = "New Hampshire"; 
				break;
			case "NJ" : 
				State = "New Jersey"; 
				break;
			case "NM" : 
				State = "New Mexico"; 
				break;
			case "NY" : 
				State = "New York"; 
				break;
			case "NC" : 
				State = "North Carolina"; 
				break;
			case "ND" : 
				State = "North Dakota"; 
				break;
			case "MP" : 
				State = "Northern Mariana Islands"; 
				break;
			case "OH" : 
				State = "Ohio"; 
				break;
			case "OK" : 
				State = "Oklahoma"; 
				break;
			case "OR" : 
				State = "Oregon"; 
				break;
			case "PW" : 
				State = "Palau"; 
				break;
			case "PA" : 
				State = "Pennsylvania"; 
				break;
			case "PR" : 
				State = "Puerto Rico"; 
				break;
			case "RI" : 
				State = "Rhode Island"; 
				break;
			case "SC" : 
				State = "South Carolina"; 
				break;
			case "SD" : 
				State = "South Dakota"; 
				break;
			case "TN" : 
				State = "Tennessee"; 
				break;
			case "TX" : 
				State = "Texas"; 
				break;
			case "UT" : 
				State = "Utah"; 
				break;
			case "VT" : 
				State = "Vermont"; 
				break;
			case "VI" : 
				State = "Virgin Islands"; 
				break;
			case "VA" : 
				State = "Virginia"; 
				break;
			case "WA" : 
				State = "Washington"; 
				break;
			case "WV" : 
				State = "West Virginia"; 
				break;
			case "WI" : 
				State = "Wisconsin"; 
				break;
			case "WY" : 
				State = "Wyoming"; 
				break;
			case "AA" : 
				State = "Armed Forces Americas"; 
				break;
			case "AP" : 
				State = "Armed Forces Pacific"; 
				break;
			default :
				WasCorrected = false;
				break;
			}
			return new Tuple<bool, string>(WasCorrected, State);
		}

	}
}

