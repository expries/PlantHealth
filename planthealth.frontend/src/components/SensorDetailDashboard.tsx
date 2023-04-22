import LargeHeading from '@/ui/LargeHeading'
import Paragraph from "@/ui/Paragraph"
import Table from "@/ui/Table"

const SensorDetailDashboard = async ({ serialNumber }: string) => {
  const baseUrl = process.env.API_BASE_URL
  const data = await fetch(`${baseUrl}/SensorData/${serialNumber}`)
  const result = await data.json()

  const sensorDataModels = result.map((req: any) => ({
    ...req
  }))

  return (
    <div className='container flex flex-col gap-6'>
      <LargeHeading>PlantHealth Sensor Data</LargeHeading>

      <Paragraph className='text-center md:text-left mt-4 -mb-4'>
        Sensor Data for SerialNumber:
      </Paragraph>

      <Table sensorDataModels={sensorDataModels} redirectOnClick={false} />
    </div>
  )
}

export default SensorDetailDashboard