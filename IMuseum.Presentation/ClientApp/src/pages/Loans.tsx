import { Card, CardFooter, CardHeader } from "../ui-components/molecules/Card";
import { Table } from "../ui-components/organisms/Table";
import { Badge } from "../ui-components/atoms/Badge";
import { PaginationComponent } from "../ui-components/molecules/Pagination";
import { useLoansPaginated } from "../hooks/useLoans";
import dayjs from "dayjs";
import { Popover } from "../ui-components/atoms/Popover";
import { LoanFilterForm } from "../ui-components/organisms/LoanFilterForm";
import { useNavigate } from "react-router-dom";
import { useSession } from "../hooks/useSession";
import { useEffect } from "react";

const Loans = () => {
  const navigate = useNavigate()
  const {
    user
  } = useSession()
  useEffect(()=>{
    if(user.role !== 'Director'){
    navigate('/home')
  }})
  const {
    data,handleChangeFilters
  } = useLoansPaginated()

  return (
    <Card className="w-full">
      <CardHeader title="Loans">
      <Popover
          render={({ open, close }) => (
            <div className="p-5">
              <LoanFilterForm onSubmit={handleChangeFilters}/>
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
          data={data?.loans ?? []}
          columns={[
            {
              name: "Status",
              align: "center",
              render: (loan) => (
                <Badge
                  color={
                    dayjs(loan.startDate).add(loan.loanApplication.duration,'months').isBefore(dayjs()) ? "danger-light" : "success-light"
                  }
                  textColor={
                    dayjs(loan.startDate).add(loan.loanApplication.duration,'months').isBefore(dayjs()) ? "danger-accent" : "success-dark"
                  }
                >
                  { dayjs(loan.startDate).add(loan.loanApplication.duration,'months').isBefore(dayjs()) ? 'Expired' : 'Loaned'}
                </Badge>
              ),
            },
            {
              name: "Artwork",
              align: "center",
              render: (loan) => (
                <div className="felx flex-col">
                  <span className="block text-xl font-semibold">
                    {loan.loanApplication.artwork.title}
                  </span>
                  <span className="uppercase text-gray-200">{loan.loanApplication.artwork.author}</span>
                </div>
              ),
            },
            {
              name: "Museum",
              align: "center",
              render: (loan) => <div>{loan.loanApplication.museum}</div>,
            },
            {
              name: "Daterange",
              align: "center",
              render: (loan) => (
                <div className="flex flex-col gap-2">
                  <div>
                    <Badge>
                      {dayjs(loan.startDate).format("DD MMM YYYY")}
                    </Badge>
                  </div>
                  <div>
                    <Badge>
                      {dayjs(loan.startDate).add(loan.loanApplication.duration,'months').format('DD MMM YYYY')}
                    </Badge>
                  </div>
                </div>
              ),
            },
            {
              name: "Income",
              align: "center",
              render: (loan) => (
                <div>
                  {loan.paymentAmount}
                </div>
              ),
            },
          ]}
        />
      </div>
      <CardFooter>
          <PaginationComponent />
        </CardFooter>
      </Card>
    );
  };

  export default Loans;
