
import { useTotals } from "../hooks/useTotals";


const Dashboard = () => {

  const calculatePercent=(total:number, ammount:number):number=>{
    return  Math.trunc(100*ammount/total)
  }

  const {
    totals,
    loading,
  } = useTotals()
  console.log(totals)
  // console.log(SessionStore.load(SessionKey.Profile))
  return (
    <div>
      {loading ? <div>Loading...</div> : (
        <>
        <div className="flex h-min gap-10">
          <div className="items-center shadow-lg flex justify-between bg-gray-700 p-8 rounded-2xl gap-5 w-[450px]">
            <div className="flex flex-col">
              <span className="text-3xl font-bold">Restoration</span>
              <span className="uppercase mb-5">ratio</span>
              <span>Percent of the artworks that are in good state.</span>
            </div>
            <div className="text-7xl items-center text-primary">
              <span >{calculatePercent(totals?.totalArtworks??1, (totals?.totalArtworks??1)-(totals?.countInRestoration??1))}</span>
              <span className="text-4xl">%</span>
            </div>
          </div> 

          <div className="items-center shadow-lg flex justify-between bg-gray-700 p-8 rounded-2xl gap-5 w-[450px]">
            <div className="flex flex-col">
              <span className="text-3xl font-bold">Artwork Loan</span>
              <span className="uppercase mb-5">ratio</span>
              <span>Percent of the artworks that are loaned.</span>
            </div>
            <div className="text-7xl items-center text-danger">
              <span >{calculatePercent(totals?.totalArtworks??1, totals?.countOnLoan??0)}</span>
              <span className="text-4xl">%</span>
            </div>
          </div>
        </div>

        <div className="mt-20 flex justify-between">
            <div className="flex flex-col items-center bg-gray-700 p-4 w-[250px] justify-center shadow-lg rounded-lg">
              <span className="text-xl">Artworks</span>
              <span className="text-4xl font-bold text-primary">{totals?.totalArtworks??0}</span>  
            </div>

            <div className="flex flex-col items-center bg-gray-700 p-4 w-[250px] justify-center shadow-lg rounded-lg">
              <span className="text-xl">Loans</span>
              <span className="text-4xl font-bold text-success">{ totals?.countOnLoan??0}</span>  
            </div>

            <div className="flex flex-col items-center bg-gray-700 p-4 w-[250px] justify-center shadow-lg rounded-lg">
              <span className="text-xl">Loan Applications</span>
              <span className="text-4xl font-bold text-warn-accent">{totals?.countLoanApplications ?? 0}</span>  
          </div>
        </div>
        </>
      )}

    </div> 
  );
};

export default Dashboard;