import { Card, CardFooter, CardHeader } from "../ui-components/molecules/Card";
import { Table } from "../ui-components/organisms/Table";
import { Badge } from "../ui-components/atoms/Badge";
import { PaginationComponent } from "../ui-components/molecules/Pagination";

const Loans = () => {
  const loans = [
    {
      status: "Loaned",
      artwork: "The joy of creation",
      museum: "Louvre Museum - FR",
      daterange:
      {
        dateStart:"27 oct 2021",
        dateEnd:"5 oct 2025"
      },
      income: "$ 1,300,000",
      author: "Miguel Angel",
    },
    {
      status: "Expired",
      artwork: "The joy of creation",
      museum: "Louvre Museum - FR",
      daterange:
      {
        dateStart:"27 oct 2021",
        dateEnd:"5 oct 2025"
      },
      income: "$ 1,300,000",
      author: "Miguel Angel",
    }
  ];

  return (
    <Card className="w-full">
      <CardHeader title="Restoration" />
      <div>
        <Table
          data={loans}
          columns={[
            {
              name: "Status",
              align: "center",
              render: (loan) => (
                <Badge
                  color={
                    loan.status === "Expired" ? "danger-light" : "success-light"
                  }
                  textColor={
                    loan.status === "Expired" ? "danger-accent" : "success-dark"
                  }
                >
                  {loan.status}
                </Badge>
              ),
            },
            {
              name: "Artwork",
              align: "center",
              render: (loan) => (
                <div className="felx flex-col">
                  <span className="block text-xl font-semibold">
                    {loan.artwork}
                  </span>
                  <span className="uppercase text-gray-200">{loan.author}</span>
                </div>
              ),
            },
            {
              name: "Museum",
              align: "center",
              render: (loan) => <div>{loan.museum}</div>,
            },
            {
              name: "Daterange",
              align: "center",
              render: (loan) => (
                <div className="flex flex-col gap-2">
                  <div>
                    <Badge>
                      {loan.daterange.dateStart}
                    </Badge>
                  </div>
                  <div>
                    <Badge>
                      {loan.daterange.dateEnd}
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
                  {loan.income}
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
