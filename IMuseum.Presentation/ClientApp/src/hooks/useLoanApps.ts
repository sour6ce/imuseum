import { useEffect, useState } from "react"
import useSWR from "swr"
import { LoanService } from "../services/LoanService"
import { GetLoanAppsFilters } from "../services/LoanService.dto"
import { Pagination } from "../ui-components/molecules/Pagination"
import { useLastValidValue } from "./useLastValidValue"


export const useLoanAppsPaginated = (initparams?:{
  pagination?:Pagination,
  filters?: GetLoanAppsFilters
})=>{
  const [filters,setFilters] = useState<GetLoanAppsFilters>({})
  const handleChangeFilters = (newFilters:GetLoanAppsFilters)=>{
    setFilters({
      ...filters,
      ...newFilters
    })
  }
  const [pagination, setPagintaion] = useState<Partial<Pagination>>({
    pageSize: initparams?.pagination?.pageSize ?? 10,
    page: initparams?.pagination?.page ?? 1,
  })
  const handleChangePagination = (newpagination:Partial<Pagination>)=>{
    setPagintaion({
      ...pagination,
      ...newpagination
    })
  }
  const {
    isValidating,
    mutate,
    data,
  } = useSWR([pagination,filters,'loan-apps'],LoanService.getLoanApps)

  const validData = useLastValidValue(data, !data || isValidating)
  useEffect(()=>{
    if(validData?.count && pagination?.total !== validData?.count){
      handleChangePagination({
        ...pagination,
        total: validData?.count ?? 0,
      })
    }
  // eslint-disable-next-line react-hooks/exhaustive-deps
  },[validData,pagination])

  return  {
    loading: isValidating || !validData,
    data: validData,
    mutate,
    pagination,
    filters,
    handleChangePagination,
    handleChangeFilters
  }
}
