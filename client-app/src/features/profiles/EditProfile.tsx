import { Formik } from "formik";
import { observer } from "mobx-react";
import React from "react";
import { Form, Button } from "semantic-ui-react";
import MyTextArea from "../../app/common/form/MyTextArea";
import MyTextInput from "../../app/common/form/MyTextInput";
import * as Yup from "yup";
import { useStore } from "../../app/stores/store";

interface Props {
  setEditMode: (editMode: boolean) => void;
}

export default observer(function EditProfile({ setEditMode }: Props) {
  const {
    profileStore: { profile, editProfile },
  } = useStore();
  return (
    <Formik
      initialValues={{ displayName: profile?.displayName, bio: profile?.bio }}
      onSubmit={(values) => {
        editProfile(values).then(() => {
          setEditMode(false);
        });
      }}
      validationSchema={Yup.object({ displayName: Yup.string().required() })}
    >
      {({ handleSubmit, isValid, isSubmitting, dirty }) => (
        <Form className="ui form" onSubmit={handleSubmit}>
          <MyTextInput name="displayName" placeholder={"Display Name"} />
          <MyTextArea rows={3} placeholder={"Add your bio"} name="bio" />
          <Button
            floated="right"
            positive
            type="submit"
            content="Update profile"
            loading={isSubmitting}
            disabled={!dirty || !isValid}
          />
        </Form>
      )}
    </Formik>
  );
});
