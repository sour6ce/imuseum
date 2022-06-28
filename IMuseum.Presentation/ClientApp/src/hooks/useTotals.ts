import useSWR from 'swr'
import { StaticService } from '../services/StaticService'


export const useTotals = () => {
  const {
    data,
    isValidating,
    mutate
  } = useSWR(['totals'],StaticService.getTotals)

  return {
    totals: !isValidating ? data?.data : undefined,
    loading: isValidating || !data,
    mutate
  }
}
