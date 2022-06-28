import { useEffect, useState } from "react"
import useSWR from "swr"
import { LoanService } from "../services/LoanService"
import { GetLoansFilter } from "../services/LoanService.dto"
import { RestoreService } from "../services/RestoreService"
import { Pagination } from "../ui-components/molecules/Pagination"
import { useLastValidValue } from "./useLastValidValue"


export const useLoansPaginated = (initparams?:{
  pagination?:Pagination,
  filters?: GetLoansFilter
})=>{
  const [filters,setFilters] = useState<GetLoansFilter>({})
  const handleChangeFilters = (newFilters:GetLoansFilter)=>{
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
  } = useSWR([pagination,filters,'loans'],LoanService.getLoans)

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
