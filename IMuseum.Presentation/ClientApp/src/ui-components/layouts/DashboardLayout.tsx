import { Outlet } from "react-router-dom"


export const DashboardLayout = ()=>{
  return (
    <div className="absolute inset-0 flex flex-row overflow-hidden bg-gray-600 text-gray-50">
      <div className="w-[300px] h-full bg-gray-800 shadow-xl">
        ASIDE
      </div>
      <div className="flex flex-grow flex-col h-full overflow-hidden">
        <div className="w-full bg-gray-700 border-b-2 border-gray-800 h-16">HEADER</div>
        <div className="w-full bg-gray-700 h-14">SUBHEADER</div>
        <div className="w-full p-5 overflow-auto scrollbar">
          <div className="h-[1000px]">lasd</div>
          <Outlet/>
        </div>
      </div>
    </div>
  )
}
