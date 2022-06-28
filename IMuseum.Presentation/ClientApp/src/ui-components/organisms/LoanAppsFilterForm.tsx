
import { useLoanAppFilterForm } from '../../hooks/forms/useLoanAppFilterForm'
import { GetLoanAppsFilters} from '../../services/LoanService.dto'
import { Button } from '../atoms/Button'
import { ControlledInput } from '../atoms/ControlledInput'
import { Form } from '../atoms/Form'

export interface LoanAppsFilterFormProps {
  onSubmit: (data: GetLoanAppsFilters) => void
}

export const LoanAppsFilterForm: React.FC<LoanAppsFilterFormProps> = (props) => {
  const methods = useLoanAppFilterForm()
  return (
    <Form
      methods={methods}
      onSubmit={(data)=>{props.onSubmit(data as GetLoanAppsFilters)}}
      className="flex flex-col items-center justify-center p-5"
    >
      <div className='w-full grid grid-cols-1 py-2'>
        <ControlledInput
          name='ArtworkId'
          placeholder='ArtworkId'
          type='text'
          className='w-full'
        />
      </div>
      <div className=' w-full grid grid-cols-1 py-2'>
        <ControlledInput
          type='text'
          name='MuseumId'
          placeholder='MuseumId'
          className='w-full'
        />
      </div>
      <div className=' w-full grid grid-cols-1 py-2'>
      <ControlledInput
          type='text'
          name='Status'
          placeholder='Status'
          className='w-full'
        />
      </div>
      <div className='w-full flex flex-row justify-start py-2'>
        <Button
          type='submit'
          onClick={()=>{return}}
        >
          Filter
        </Button>
      </div>
    </Form>
  )
}
