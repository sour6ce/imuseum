
import { useLoanFilterForm } from '../../hooks/forms/useLoanFilterForm'
import { GetLoansFilter } from '../../services/LoanService.dto'
import { Button } from '../atoms/Button'
import { ControlledInput } from '../atoms/ControlledInput'
import { Form } from '../atoms/Form'

export interface LoanFilterFormProps {
  onSubmit: (data: GetLoansFilter) => void
}

export const LoanFilterForm: React.FC<LoanFilterFormProps> = (props) => {
  const methods = useLoanFilterForm()
  return (
    <Form
      methods={methods}
      onSubmit={(data)=>{props.onSubmit(data as GetLoansFilter)}}
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
      <div className=' w-full grid grid-cols-2 py-2'>
        <ControlledInput
          type='number'
          name='IncomeMin'
          label='Income Min'
          className='w-full'
        />
        <ControlledInput
          type='number'
          name='IncomeMax'
          label='Income Max'
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
