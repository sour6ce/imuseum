import { Card, CardFooter, CardHeader } from "../ui-components/molecules/Card";
import { Table } from "../ui-components/organisms/Table";
import { Badge } from "../ui-components/atoms/Badge";
import { PaginationComponent } from "../ui-components/molecules/Pagination";
import { Button } from "../ui-components/atoms/Button";
import Icon from "../ui-components/atoms/Icon";
import { selectStatusColor } from "../utils/selectStatusColor";
import { useLoanAppsPaginated } from "../hooks/useLoanApps";
import { LoanService } from "../services/LoanService";
import { Popover } from "../ui-components/atoms/Popover";
import Input from "../ui-components/atoms/Input";
import { useEffect, useState } from "react";
import { LoanAppsFilterForm } from "../ui-components/organisms/LoanAppsFilterForm";
import { useNavigate } from "react-router-dom";
import { useSession } from "../hooks/useSession";

const PopoverContent = ({ open, close, id,handleClick }) => {
  const [value, setValue] = useState('')
  return(
  <div className="p-5 flex flex-row gap-2">
    <Input
      type="number"
      placeholder="Payment"
      onChange={(e)=>{setValue(e.target.value)}}
    />
    <Button
      onClick={handleClick(id,value)}
    >
      Accept
    </Button>
  </div>
)}

const LoansAplications = () => {
  const navigate = useNavigate()
  const {
    user
  } = useSession()
  useEffect(()=>{
    if(user.role !== 'Director'){
    navigate('/home')
  }})
  
  const handleCancelLoad=(id)=>{
    LoanService.rejectLoanApp(id).then(()=>{
      mutate()
    })
  }
  const handleOrderArtwork=(id,pay)=>{
    LoanService.acceptLoanApp(id,pay).then(()=>{
      mutate()
    })
  }

  const {
    data,mutate,handleChangeFilters
  } = useLoanAppsPaginated()

  return (
    <Card className="w-full">
      <CardHeader title="Loan Apps">
        <Popover
          render={({ open, close }) => (
            <div className="p-5">
              <LoanAppsFilterForm onSubmit={handleChangeFilters}/>
            </div>
          )}
          buttonProps={{}}
          position="right"
        >
          Filter
        </Popover>
      </CardHeader>
      <div>
        <Table
          data={data?.loanApps ?? []}
          columns={[
            {
              name:"Actions",
              align:"center",
              render:(loan) =>(
                <div className="flex gap-2 justify-center">
                  {loan.loanApplicationStatus === 'on-wait'?
                  <Popover
                    render={({open,close})=><PopoverContent
                      close={close}
                      open={open}
                      id={loan.id}
                      handleClick={handleOrderArtwork}
                    />}
                    buttonProps={{color:'success'}}
                    position="left"
                  >
                    <Icon family="fontawesome" name="check" />
                  </Popover>:<></>}

                  <Button  color="danger" onClick={()=>handleCancelLoad(loan.id)}>
                    {<Icon family="fontawesome" name="circle-minus" />}
                  </Button>
                </div>
              )
            },
            {
              name: "Status",
              align: "center",
              render: (loan) => (
                <Badge
                  color={selectStatusColor(loan.loanApplicationStatus+"")}
                >
                  {loan.loanApplicationStatus}
                </Badge>
              ),
            },
            {
              name: "Artwork",
              align: "center",
              render: (loan) => (
                <div className="felx flex-col">
                  <span className="block text-xl font-semibold">
                    {loan.artwork.title}
                  </span>
                  <span className="uppercase text-gray-200">{loan.artwork.author}</span>
                </div>
              ),
            },
            {
              name: "Museum",
              align: "center",
              render: (loan) => <div>{loan.museum}</div>,
            },
            {
              name: "Duration",
              align: "center",
              render: (loan) => <div>{loan.duration}</div>,
            }
          ]}
        />
      </div>
      <CardFooter>
          <PaginationComponent />
        </CardFooter>
      </Card>
    );
  };

  export default LoansAplications;
