use std::env;
use std::fs;
use std::path::Path;

fn main() {
    let dirs_and_files: Vec<String> = env::args().collect();

    for p in dirs_and_files {
        // Get the path
        let content = Path::new(&p);

        if !content.exists() {
            panic!("The path {} does not exist!", p);
        }

        
    }
}
