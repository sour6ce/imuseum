
export interface DashboardSubheaderProps{
  title?:string,
}

const DashboardSubheader:React.FC<DashboardSubheaderProps> = (props)=>{
  return (
    <div className=" bg-gray-700 shadow-md px-10 py-2 items-center text-primary-light flex justify-between">
      <div className="flex py-2">
        
      </div>
      <div id='subheader-portal-container'/>
    </div>
  )
}
export default DashboardSubheader;