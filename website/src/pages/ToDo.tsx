import { Avatar, Box, Divider, IconButton, List, ListItem, ListItemAvatar, ListItemText, Paper, Snackbar, styled } from "@mui/material";
import * as React from "react";
import { useContext, useEffect } from "react";
import DeleteIcon from "@mui/icons-material/Delete";
import CheckIcon from '@mui/icons-material/Check';
import CloseIcon from '@mui/icons-material/Close';
import '../App.css';
import TextField from '@mui/material/TextField';
import { Formik } from "formik";
import { CreateToDo, DeleteToDo, GetToDoList, UpdateCompletedStatus } from "../services/ToDoService";
import { format } from 'date-fns';



export default function ToDo() {
    const [formData, setFormData] = React.useState({ title: '' });
    const [todoList, setToDoList] = React.useState([]);
    const [notification, setNotification] = React.useState({open:false, errorMessage: ''});

    const handleClose = (event: React.SyntheticEvent | Event, reason?: string) => {
        if (reason === 'clickaway') {
        return;
        }

        setNotification({...notification, open:false, errorMessage:''});
    };



    async function handleToDoOnClick(id: any, isCompleted: boolean) {
        try {
            const result: any = await UpdateCompletedStatus({
                toDoId: id,
                isCompleted: !isCompleted
            });

            fetchToDoList();


        } catch (error) {
            if (error) {
                setNotification({
                    open: true,
                    errorMessage: error as string,
                });
            }
        }
    }

    async function handleDeleteOnClick(id: any) {
        try {
            const result: any = await DeleteToDo({
                toDoId: id
            });

            fetchToDoList();


        } catch (error) {
            if (error) {
                setNotification({
                    open: true,
                    errorMessage: error as string,
                });
            }
        }
    }

    async function submitToDo(values: any) {

        var title = values.title;
        try {
            const result: any = await CreateToDo({
                title: title
            });
            setFormData({ ...formData, title: '' });
            fetchToDoList();


        } catch (error) {
            if (error) {
                setNotification({
                    open: true,
                    errorMessage: error as string,
                });
            }
        }
    }

    async function fetchToDoList() {
        try {
            //new http for API
            let params = new URLSearchParams();
            const result: any = await GetToDoList(params);
            if (result && result.data.data) {
                console.log(result);
                setToDoList(result.data.data);
            } else {
                console.log(result);
                setNotification({
                    open: true,
                    errorMessage: result.data.error as string,
                });
            }
        } catch (error) {
            
            if (error) {
                setNotification({
                    open: true,
                    errorMessage: error as string,
                });
            }
        }
    }

    useEffect(() => {
        fetchToDoList();
    }, []);

    useEffect(() => {

    }, [formData,notification]);


    const action = (
        <React.Fragment>
          <IconButton
            size="small"
            aria-label="close"
            color="inherit"
            onClick={handleClose}
          >
            <CloseIcon fontSize="small" />
          </IconButton>
        </React.Fragment>
      );

      
    return (
        <Box
            className="App-container"
        >
            <h1 style={{ textAlign: "left" }}>
                ToDos
            </h1>
            <Formik
                initialValues={formData}
                onSubmit={(values, { resetForm }) => {
                    if (values.title.length > 0) {
                        submitToDo(values);
                        resetForm();
                    }
                }}
            >
                {props => {
                    const {
                        values,
                        handleChange,
                        handleBlur,
                        handleSubmit,
                    } = props;
                    return (
                        <form
                            onSubmit={handleSubmit}
                            onKeyDown={(e) => {
                                if (e.key === 'Enter') {
                                    e.preventDefault();
                                    handleSubmit();
                                }
                            }}
                            className="App-wrapper"
                        >
                            <div className="App-wrapper" >
                                <TextField
                                    id="title"
                                    label=""
                                    fullWidth
                                    placeholder="e.g. Make a call"
                                    variant="outlined"
                                    value={values.title}
                                    onChange={handleChange}
                                    onBlur={handleBlur} />
                            </div>
                            <br />
                        </form>
                    )
                }}
            </Formik>

            <Paper className="App-wrapper" >
                {
                    (todoList && todoList.length > 0) ?
                        (
                            <List className="App-list" style={{ borderRadius: 4, borderWidth: 1 }}>
                                {todoList.map((item: any, index: any) => {
                                    return (
                                        <div key={index.toString()} >
                                            {(index > 0) ? <Divider key={'d' + index.toString()} /> : ''}
                                            <ListItem
                                                id={index.toString()}
                                                key={index.toString()}
                                                className={(item.isCompleted == true) ? "App-mark-complete" : "App-mark-incomplete"}
                                                secondaryAction={
                                                    <IconButton edge="end" aria-label="delete" onClick={() => handleDeleteOnClick(item.toDoId)}>
                                                        <DeleteIcon />
                                                    </IconButton>
                                                }
                                                onClick={() => handleToDoOnClick(item.toDoId, item.isCompleted)}
                                            >
                                                <ListItemAvatar>
                                                    <CheckIcon className="App-check-mark" />
                                                </ListItemAvatar>
                                                <ListItemText
                                                    primary={item.title}
                                                    secondary={format(new Date(item.createdDate), "MM/dd/yyyy, hh:mm a")}
                                                />
                                            </ListItem>
                                        </div>
                                    );
                                })}

                            </List>
                        ) :
                        (
                            <div className="App-empty-message" style={{ borderRadius: 4, borderWidth: 1 }}>To Do list is empty!</div>
                        )
                }
            </Paper>
            <Snackbar
                open={notification.open}
                autoHideDuration={6000}
                onClose={handleClose}
                message={notification.errorMessage}
                action={action}
            />
        </Box>

    );
}
