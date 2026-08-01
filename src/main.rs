use core::error;
use std::env;
use filesize::PathExt;
use std::collections::HashMap;
use std::path::Path;

fn get_file_size(file: &Path) -> Result<u64, String> {
    let size = file.size_on_disk();

    match size {
        Ok(s) => { return Ok(s); },
        Err(e) => { 
            let error_message = format!("An error occurent while reading the file:\n{}", e.to_string());
            return Err(error_message); 
        }
    }
}

fn get_dir_size(file: &Path) -> Result<u64, String> {
    Err("This application cannot read directories yet.".to_string())
}

fn main() {
    let mut file_compilations: HashMap<String, String> = HashMap::new();
    let dirs_and_files: Vec<String> = env::args().collect();

    for p in dirs_and_files {
        // Get the path
        let content = Path::new(&p);

        if !content.exists() {
            panic!("The path {} does not exist!", p);
        }

        let mut content_size: Result<u64, String>;

        if content.is_file() {
            content_size = get_file_size(content);

            if content_size.is_ok() {
                file_compilations.insert(p.to_string(), content_size.ok().expect("An unexpected error occured while getting file data.").to_string());
            }
            else {
                //TODO: Implement folder reading
            }

            
        } else if content.is_dir() {
            content_size = get_dir_size(content);
        }


    }
}
