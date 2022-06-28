import { Card, CardFooter, CardHeader } from "../ui-components/molecules/Card";
import { Table } from "../ui-components/organisms/Table";
import { Badge } from "../ui-components/atoms/Badge";
import { PaginationComponent } from "../ui-components/molecules/Pagination";
import { Button } from "../ui-components/atoms/Button";
import Icon from "../ui-components/atoms/Icon";
import { selectStatusColor } from "../utils/selectStatusColor";

const LoansAplications = () => {
  
  const handleCancelLoad=()=>{

  }
  const handleOrderArtwork=()=>{

  }

  const loans = [
    {
      status: "available",
      artwork: "The joy of creation",
      museum: "Louvre Museum - FR",
      duration:"2 years",
      author: "Miguel Angel",
    },
    {
      status: "unavailable",
      artwork: "The joy of creation",
      museum: "Louvre Museum - FR",
      duration:"2 years",
      author: "Miguel Angel",
    },
    {
      status: "canceled",
      artwork: "The joy of creation",
      museum: "Louvre Museum - FR",
      duration:"2 years",
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
              name:"Actions",
              align:"center",
              render:(loan) =>(
                <div className="flex gap-2 justify-center">
                  {loan.status==="available"?
                  <Button color="success" onClick={handleOrderArtwork}>
                    {<Icon family="fontawesome" name="check" />}
                  </Button>:<></>}

                  {(loan.status==="available" || loan.status==="unavailable" )?
                  <Button  color="danger" onClick={handleCancelLoad}>
                    {<Icon family="fontawesome" name="circle-minus" />}
                  </Button>:<></>}



                </div>
              )
            },
            {
              name: "Status",
              align: "center",
              render: (loan) => (
                <Badge
                  color={selectStatusColor(loan.status)}
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
