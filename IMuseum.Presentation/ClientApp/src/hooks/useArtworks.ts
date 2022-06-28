import { useState, useEffect } from "react"
import useSWR from "swr"
import { ArtworkService } from "../services/ArtworkService"
import { GetArtworksFilter } from "../services/ArtworkService.dto"
import { Pagination } from "../ui-components/molecules/Pagination"
import { useLastValidValue } from "./useLastValidValue"


export const useArtworkById = (id: string) => {
  const {
    isValidating,
    data,
    mutate,
    error,
  } = useSWR([id,'artwork'],ArtworkService.getArtworkById)

  const validData = useLastValidValue(data, !data || isValidating)

  return {
    loading: isValidating || !validData,
    data: validData,
    mutate, 
    error
  }
}

export const useArtworksPaginated = (initparams?:{
  pagination?:Pagination,
  filters?: GetArtworksFilter
})=>{
  const [filters,setFilters] = useState<GetArtworksFilter>({})
  const handleChangeFilters = (newFilters:GetArtworksFilter)=>{
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
  } = useSWR([pagination,filters,'artworks'],ArtworkService.getArtworks)

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