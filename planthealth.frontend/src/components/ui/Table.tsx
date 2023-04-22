'use client'

import { GridColumnHeaderParams, GridColDef, DataGrid, GridRowParams } from '@mui/x-data-grid'
import { FC } from 'react'
import { useRouter } from 'next/navigation'

const columnsDraft: GridColDef[] = [
    {
        field: 'col1',
        headerName: 'Serial Number',
        width: 350,
        renderHeader(params) {
            return (
                <strong className='font-semibold'>{params.colDef.headerName}</strong>
            )
        }
    },
    {
        field: 'col2',
        headerName: 'Soil Humidity',
        width: 250,
    },
    {
        field: 'col3',
        headerName: 'Temperature',
        width: 250,
    },
    {
        field: 'col4',
        headerName: 'Air Humidity',
        width: 250,
    },
    {
        field: 'col5',
        headerName: 'Date',
        width: 250,
    }
]

const columns = columnsDraft.map(col => {
    if (col.field == 'col1')
        return col

    return {
        ...col,
        renderHeader(params: GridColumnHeaderParams<any, any, any>) {
            return (
                <strong className='font-semibold'>{params.colDef.headerName}</strong>
            )
        }
    }
})

type SensorDataModel = {
    id: string,
    serialNumber: string,
    soilHumidity: number,
    temperature: number,
    airHumidity: number,
    timestamp: Date
}

interface TableProps {
    sensorDataModels: [SensorDataModel],
    redirectOnClick: boolean
}

const Table: FC<TableProps> = ({ sensorDataModels, redirectOnClick }) => {
    const router = useRouter()

    const rows = sensorDataModels.map(sensorData => ({
        id: sensorData.id,
        col1: sensorData.serialNumber,
        col2: sensorData.soilHumidity.toFixed(6),
        col3: sensorData.temperature.toFixed(6),
        col4: sensorData.airHumidity.toFixed(6),
        col5: new Date(sensorData.timestamp).toLocaleString("de-AT")
    }))

    const SensorDataDetails = (params: GridRowParams) => {
        if(!redirectOnClick)
            return;

        const serialNumber = params.row.col1
        router.push(`/sensorDetails/${serialNumber}`)
    }

    return (<DataGrid style={{ backgroundColor: 'white', fontSize: '1rem' }}
        pageSizeOptions={[10]}
        disableRowSelectionOnClick
        autoHeight
        initialState={{
            pagination: {
                paginationModel: {
                    pageSize: 10
                },
            },
        }} columns={columns}
        rows={rows}
        onRowClick={SensorDataDetails} />)
}

export default Table