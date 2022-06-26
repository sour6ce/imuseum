import { useContext, useEffect } from "react";
import { useLocation } from "react-router-dom";
import { BreadcrumbItemProps } from "../atoms/BreadcrumbItem";
import { asideItems } from "../layouts/DashboardLayout";
import { SubheaderContext } from "../layouts/SubheaderProvider";
import { Breadcrumb } from "../molecules/Breadcrumb";

export interface DashboardSubheaderProps{
  title?:string,
}

const DashboardSubheader:React.FC<DashboardSubheaderProps> = (props)=>{
  const {
    breadcrumbs,
    addManyBreadcrumbs,
    deleteManyBreadcrumbs

  } = useContext(SubheaderContext)
  const location = useLocation()
  useEffect(()=>{
    const matching = asideItems.filter(i => location.pathname.match(i.to))
    const breads : BreadcrumbItemProps[] = matching.map(i => ({
      path: i.to,
      name: i.name,
    }))
    addManyBreadcrumbs(breads)
    return ()=>{
      deleteManyBreadcrumbs(breads)
    }
  },[addManyBreadcrumbs, deleteManyBreadcrumbs, location])

  return (
    <div className=" bg-gray-700 shadow-md px-10 py-2 items-center text-primary-light flex justify-between">
      <div className="flex py-2">
        <Breadcrumb items={breadcrumbs}/>
      </div>
      <div id='subheader-portal-container'/>
    </div>
  )
}
export default DashboardSubheader;