<template>
  <div class="m-0 mt-5">
    <b-row v-if="success !== null" class="upload-status pb-3">
      <h2 v-if="success === true" class="text-success text-left">Success</h2>
      <h2 v-else-if="success === false" class="text-danger text-left">
        Error: {{error}}
      </h2>
    </b-row>
    <div v-if="isIdInvalid">
      <h2 class="text-danger text-left">Error: ItemId is Required</h2>
    </div>
    <div v-else>
      <b-row>
        <b-col cols="4" class="p-0">
          <b-form-file
            v-model="file"
            :state="Boolean(file)"
            @input="fileUploaded"
            placeholder="Choose a file or drop it here..."
            drop-placeholder="Drop file here..."
            class="text-left"
          ></b-form-file>
        </b-col>
      </b-row>
      <b-row>
        <div class="mt-3">
          <p>Selected file: {{ file ? file.name : '' }}</p>
        </div>
      </b-row>
      <b-row>
        <div class="mt-3">
          <b-button class="btn-success" :disabled="!file" @click="upload">Upload</b-button>
        </div>
      </b-row>
    </div>
  </div>
</template>

<script src="./upload.js">
</script>